import os
from os.path import isfile, join
import json
import pymongo
from pymongo import MongoClient
import pandas


def import_file(pth, fname):
    coll_name = fname.replace('.json', '')
    print(fname, coll_name)
    client = MongoClient()
    db = client['northwind']

    with open(join(pth,fname)) as f:
        coll = db[coll_name]
        data = json.load(f)
        coll.insert_many(data)
        print(f'inserted collection {coll_name} - {len(data)}')

    client.close()


def list_collections():
    client = MongoClient()
    db = client['northwind']
    colls = db.list_collections()
    for c in colls:
        print(c['name'])


def show_collection(coll_name):
    client = MongoClient()
    db = client['northwind']
    coll = db[coll_name]
    ret = coll.find()
    for r in ret:
        print(r)


# Press the green button in the gutter to run the script.
if __name__ == '__main__':
    # pth = 'c:/dev/mongolab/data-json'
    # files = [fl for fl in os.listdir(pth) if isfile(join(pth, fl))]
    # for fl in files:
    #     import_file(pth, fl)

    # list_collections()

    show_collection('product')