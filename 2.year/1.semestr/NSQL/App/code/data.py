from flask import Flask, render_template, request, redirect, flash, url_for, make_response
from bson import json_util
from pymongo import MongoClient

#Fill the specified collections with predefined data lists.
def fill_data(col_list):

    weapons = [
  { "id": "0","name": "Dagger", "dmg_dice": "1d4 piercing", "Properties": ["finesse","light","thrown(20/60)"]},
  { "id": "1","name": "Shortsword", "dmg_dice": "1d6 piercing	", "Properties": ["finesse","light"]},
  { "id": "2","name": "Greatsword", "dmg_dice": "2d6 slashing", "Properties": ["Heavy", "two-handed"]},
  { "id": "3","name": "Longbow", "dmg_dice": "1d8 piercing", "Properties": ["Ammunition", "range (150/600)", "heavy", "two-Handed"]},
      ]
    cantrips = [
  { "id": "0","name": "Acid Splash", "school": "Conjuration", "casting time": "1 Action", "range": "60 Feet"},
  { "id": "1","name": "Blade Ward", "school": "Abjuration", "casting time": "1 Action", "range": "Self"},
  { "id": "2","name": "Booming Blade", "school": "Evocation", "casting time": "1 Action", "range": "Self (5-foot radius)"},
  { "id": "3","name": "Control Flames", "school": "Necromancy", "casting time": "1 Action", "range": "120 feet"},
  { "id": "4","name": "Create Bonfire", "school": "Transmutation", "casting time": "1 Action", "range": "60 Feet"},
    ]

    spellslvl1 = [
  { "id": "0","name": "Absorb Elements", "school": "Abjuration", "casting time": "1 Reaction", "range": "Self"},
  { "id": "1","name": "Alarm", "school": "Abjuration", "casting time": "1 Minute", "range": "30 feet"},
  { "id": "2","name": "Animal Friendship", "school": "Enchantment", "casting time": "1 Action", "range": "30 feet"},
  { "id": "3","name": "Armor of Agathys", "school": "Abjuration", "casting time": "1 Action", "range": "Self"},
  { "id": "4","name": "Arms of Hadar", "school": "Conjuration", "casting time": "1 Action", "range": "Self (10-foot radius)"},
    ]

    spellslvl2 = [
  { "id": "0","name": "Aganazzar's Scorcher", "school": "Evocation", "casting time": "1 Action", "range": "30 Feet"},
  { "id": "1","name": "Aid", "school": "Abjuration", "casting time": "1 Action", "range": "30 Feet"},
  { "id": "2","name": "Air Bubble", "school": "Conjuration", "casting time": "1 Action", "range": "60 Feet"},
  { "id": "3","name": "Alter Self", "school": "Transmutation", "casting time": "1 Action", "range": "Self"},
  { "id": "4","name": "Animal Messenger", "school": "Enchantment", "casting time": "1 Action", "range": "30 Feet"},
    ]

    players = [
  { "id": "0", 
   "username": "venca",
   "name": "Venca", 
   "race": "Half-elf", 
   "class": "Ranger", 
   "level": "3", 
   "profile_img": "https://media.tenor.com/EloqEPT0SNYAAAAM/frieren-owo-moment-lil-cat-face.gif",
   "char_stats":{"strength": {"stat":"5", "bonus":"1"},
                 "dexterity": {"stat":"6", "bonus":"2"},
                 "constitution": {"stat":"7", "bonus":"3"},
                 "intelligence": {"stat":"8", "bonus":"4"},
                 "wisdom": {"stat":"9", "bonus":"5"},
                 "charisma": {"stat":"10", "bonus":"6"}}, 
   "fight_stats": {"armor_class": "12",
                   "initiative": "7",
                   "speed": "30",
                   "max_hitpoints": "18",
                   "current_hitpoints": "10"},
   "spells":{"cantrips": ["1","2","3"], "spellslvl1": ["1","2","4"], "spellslvl2": ["2","3","1"]},
    "equipment": {"weapons": ["1","3"], "armor": [""], "another": [""]} },
  { "id": "1", 
   "username": "bao",
   "name": "Bao", 
   "race": "Elf", 
   "class": "Warrior", 
   "level": "3", 
   "profile_img": "https://i.pinimg.com/736x/98/51/75/985175b84cf1a5f4c2f782592574c0da.jpg",
   "char_stats":{"strength": {"stat":"5", "bonus":"1"},
                 "dexterity": {"stat":"6", "bonus":"2"},
                 "constitution": {"stat":"7", "bonus":"3"},
                 "intelligence": {"stat":"8", "bonus":"4"},
                 "wisdom": {"stat":"9", "bonus":"5"},
                 "charisma": {"stat":"10", "bonus":"6"}}, 
   "fight_stats": {"armor_class": "12",
                   "initiative": "7",
                   "speed": "30",
                   "max_hitpoints": "18",
                   "current_hitpoints": "10"},
   "spells":{"cantrips": ["2","1","4"], "spellslvl1": ["1","2","4"], "spellslvl2": ["2","3","1"]},
   "equipment": {"weapons": ["1","3"], "armor": [""], "another": [""]} },
    ]


    data_list = [cantrips, spellslvl1, spellslvl2, players,weapons]
    for key,value in col_list.items():
        value.delete_many({}) 
        value.insert_many(data_list[list(col_list.keys()).index(key)])
    return data_list

# Filter the list of dictionaries based on the provided IDs.
def getData(dictId,dictValues):
    mylist = []

    for d in dictValues:
      for id in dictId:
        if d["id"] == id:
          mylist.append(d)     
    return mylist
             