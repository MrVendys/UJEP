from flask import Flask, render_template, request, redirect, flash, url_for, make_response
from flask import render_template
from flask_login import LoginManager, login_user, UserMixin, login_required, logout_user, current_user
import pymongo
from bson import json_util
from pymongo import MongoClient
import data
import db
import sys

#Vytvoreni databaze a kolekci
col_list = db.create_db()

#naplneni kolekci daty
data_list = data.fill_data(col_list)
app = Flask(__name__, template_folder='static/templates')
app.secret_key = "super secret key"

#--------------------------Login----------------------------------
# Definovani user tridy
class User(UserMixin):
    def __init__(self, id):
        self.id = id

# vytvoreni loginu
login_manager = LoginManager()
login_manager.init_app(app)

# Specifikace vlastního spravce, ktery uzivatele presmeruje na prihlasovaci stranku
@login_manager.unauthorized_handler
def unauthorized_callback():
    return redirect(url_for('login'))

# Load user from the database based on username
# nacteni uzivatele z databaze podle jmena
@login_manager.user_loader
def load_user(username):
    player = col_list["players"].find_one({"username": username})
    if player:
        return User(username)  # Predpoklada se, ze jmeno je v databazi jedinecne
    return None

# Modify the login route to authenticate based on the username
# Reseni prihlasovani na specifickych routach
@app.route("/",methods=['GET', 'POST'])
@app.route("/login", methods=['GET', 'POST'])
def login():
    # Prihlasovani podle jmena
    global logged_user_id
    if request.method == 'POST':
        username = request.form['username'].lower()
        #Najde hrace podle jmena
        player = next((p for p in col_list["players"].find({"username": {"$regex": f"^{username}$", "$options": "i"}})), None)
        if player:
            login_user(User(username))  # Prihlasi hrace
            logged_user_id = player["id"]
            # Presmeruje na route index
            return redirect(url_for('index'))
        else:
            return "Invalid username or password"
    else:
        return render_template('login.html')

#-----------------------------------------------------------------



#Tato funkce nacte data hrace pro prihlaseneho uzivatele a vykresli aablonu index.html s daty hrace.
@app.route("/home")
@app.route("/index")
@login_required
def index():
    player = col_list["players"].find_one({"id": logged_user_id})
    
    return render_template("index.html", player=player)

#Vytvoří stránku kouzel s informacemi o hráči a seznamy kouzel
@app.route('/spells')
@login_required
def spells():
    player = col_list["players"].find_one({"id": logged_user_id})
    cantrips = list(col_list["cantrips"].find())
    spellslvl1 = list(col_list["spellslvl1"].find())
    spellslvl2 = list(col_list["spellslvl2"].find())
    return render_template('spells.html', cantrips=cantrips, spellslvl1=spellslvl1,spellslvl2=spellslvl2, player = player)

#Renderování šablony my_spells.html s kouzly a seznamy kouzel hráče.
#Umožňuje hráči aktualizovat seznamy kouzel pomocí metody POST.
@app.route("/my_spells", methods=['GET', 'POST'])
@login_required
def my_spells():
    # Najde hrace pomoci logged_user_id
    player = col_list["players"].find_one({"id": logged_user_id})
    if request.method == 'POST':
        key_value = list(request.form.keys())
        new_data_list = []
        for key,value in request.form.items():
            for item in request.form.getlist(key_value[0]):
                new_data_list.append(item)
        new_data_list = [item for item in new_data_list if item != ""]
        col_list["players"].update_one({"id": logged_user_id}, {"$set": {f"spells.{key_value[0]}": new_data_list}})

    player = col_list["players"].find_one({"id": logged_user_id})
    spells_list = []
    spells_list.append(list(col_list["cantrips"].find()))
    spells_list.append(list(col_list["spellslvl1"].find()))
    spells_list.append(list(col_list["spellslvl2"].find()))
    
    splcn = data.getData(player["spells"]["cantrips"],col_list["cantrips"].find())
    splvl1 = data.getData(player["spells"]["spellslvl1"],col_list["spellslvl1"].find())
    splvl2 = data.getData(player["spells"]["spellslvl2"],col_list["spellslvl2"].find())
    return render_template("my_spells.html", player = player, cantrips = splcn, spellslvl1 = splvl1, spellslvl2 = splvl2, spells_list = spells_list)

#Stejné jako my_spells(), ale pro inventář hráče.
@app.route("/inventory", methods=['GET', 'POST'])
@login_required
def invetory():
    player = col_list["players"].find_one({"id": logged_user_id})
    if request.method == 'POST':
        key_value = list(request.form.keys())
        new_data_list = []
        for key,value in request.form.items():
            for item in request.form.getlist(key_value[0]):
                new_data_list.append(item)
        new_data_list = [item for item in new_data_list if item != ""]
        col_list["players"].update_one({"id": logged_user_id}, {"$set": {f"equipment.{key_value[0]}": new_data_list}})

    player = col_list["players"].find_one({"id": logged_user_id})
    pl_weapons = data.getData(player["equipment"]["weapons"],col_list["weapons"].find())
    weapons = list(col_list["weapons"].find())
    return render_template("inventory.html", player = player, weapons = pl_weapons, weapon_list = weapons)

if __name__ == "__main__":
    app.run(host="0.0.0.0", port=5000, debug=True)