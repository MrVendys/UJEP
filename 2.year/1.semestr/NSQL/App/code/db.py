from flask import Flask, render_template, request, redirect, flash, url_for, make_response
import pymongo
from bson import json_util
from pymongo import MongoClient

# Connects to the MongoDB and creates necessary collections/indexes.
def create_db():
    myclient = pymongo.MongoClient("mongodb://admin:admin@mongodb:27017", connect=False)
    db = myclient['DnD']
    col_cantrip = db['Cantrips']
    col_spells_lvl1 = db['Spells_lvl1']
    col_spells_lvl2 = db['Spells_lvl2']
    col_players = db['Players']
    col_weapons= db['Weapons']
    db.col_cantrip.create_index([("id", pymongo.ASCENDING)], unique=True)
    db.col_spells_lvl1.create_index([("id", pymongo.ASCENDING)], unique=True)
    db.col_spells_lvl2.create_index([("id", pymongo.ASCENDING)], unique=True)
    db.col_players.create_index([("id", pymongo.ASCENDING)], unique=True)
    db.col_player_spells.create_index([("id", pymongo.ASCENDING)], unique=True)
    db.weapons.create_index([("id", pymongo.ASCENDING)], unique=True)
    col_list = {"cantrips":col_cantrip,"spellslvl1":col_spells_lvl1,"spellslvl2":col_spells_lvl2,"players":col_players,"weapons":col_weapons}#{"col_spells_lvl1,col_spells_lvl2,col_players, col_weapons}
    return col_list
