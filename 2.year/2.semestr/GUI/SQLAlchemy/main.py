from sqlalchemy import Column, ForeignKey, Integer, String, DateTime, Enum, MetaData, func, create_engine, inspect
from sqlalchemy.orm import relationship, declarative_base, sessionmaker

Base = declarative_base()



class Zakaznik(Base):
    __tablename__ = 'zakaznik'
    
    id = Column(Integer, primary_key=True, autoincrement=True)
    jmeno = Column(String(25))
    prijmeni = Column(String(25))
    dt_create = Column(DateTime, server_default=func.now())
    
    kontakt = relationship("Zakaznik_kontakt")
    adresa = relationship("Zakaznik_adresa")
    
class Zakaznik_kontakt(Base):
    __tablename__ = 'zakaznik_kontakt'
    id = Column(Integer, primary_key=True, autoincrement=True)
    telefon = Column(String(9))
    email = Column(String(15))
    id_zak = Column(Integer, ForeignKey('zakaznik.id'))

class Zakaznik_adresa(Base):
    __tablename__ = 'zakaznik_adresa'
    id = Column(Integer, primary_key=True, autoincrement=True)
    ulice = Column(String)
    mesto = Column(String)
    psc = Column(Integer)
    trvale_bydliste = Column(Enum("Y","N"))
    id_zak = Column(Integer, ForeignKey('zakaznik.id'))


def create_tables():
    # Vytvoření in-memory databáze
    engine = create_engine('sqlite:///:memory:', echo=False)

    # Vytvoření schématu (metadata) pro tabulky
    metadata = MetaData()

    # Vytvoření tabulek
    Base.metadata.create_all(engine)

    # Získání informací o tabulkách
    inspector = inspect(engine)

    # Získání názvů všech tabulek
    table_names = inspector.get_table_names()
    print(f"Seznam tabulek: {table_names}")

    for table_name in table_names:
        # Získání informací o sloupcích
        columns = inspector.get_columns(table_name)

        # Vytisknutí informací o sloupcích
        print()
        print(f"Struktura tabulky '{table_name}':")
        for column in columns:
            print(f"Název sloupce: {column['name']}, Typ sloupce: {column['type']}")

        # Získání informací o klíčích
        primary_keys = inspector.get_pk_constraint(table_name)
        foreign_keys = inspector.get_foreign_keys(table_name)

        print(f"\nInformace o klíčích pro tabulku '{table_name}':")
        print("Primární klíče:")
        for pk in primary_keys['constrained_columns']:
            print(f" - {pk}")

        print("\nCizí klíče:")
        for fk in foreign_keys:
            print(f" - Sloupec: {fk['constrained_columns']}, Reference: {fk['referred_columns']}")


engine = create_engine('sqlite:///:memory:')
Base.metadata.create_all(engine)  # Vytvoření tabulek

Session = sessionmaker(bind=engine)
session = Session()

novi_zakaznici = [Zakaznik(jmeno='John', prijmeni='Doe'), Zakaznik(jmeno='Phill', prijmeni='Ascen')]
nove_adresy = [
    Zakaznik_adresa(ulice='Main Street', mesto='Cityville', psc='12345', trvale_bydliste='Y', id_zak=1),
    Zakaznik_adresa(ulice='Maple Avenue', mesto='Townsville', psc='54321', trvale_bydliste='N', id_zak=2),
    Zakaznik_adresa(ulice='Oak Street', mesto='Villageton', psc='98765', trvale_bydliste='Y', id_zak=3),
    Zakaznik_adresa(ulice='Cedar Road', mesto='Hamletville', psc='13579', trvale_bydliste='N', id_zak=4),
    Zakaznik_adresa(ulice='Pine Lane', mesto='Burgville', psc='24680', trvale_bydliste='Y', id_zak=5)
]
session.add_all(nove_adresy)
session.add_all(novi_zakaznici)
session.commit()

session = Session()
zakaznici = session.query(Zakaznik).all()
session.commit()

session = Session()
result = session.query(Zakaznik).all()
##for row in result:
##    print(row.jmeno, row.prijmeni)

result = session.query(Zakaznik_adresa).filter(Zakaznik_adresa.trvale_bydliste == "Y").all()
##for row in result:
##    print(row.ulice, row.mesto)

result = session.query(Zakaznik_kontakt).order_by(Zakaznik_kontakt.email.asc()).all()
##for row in result:
##    print(row.ulice, row.mesto)

session.commit()

session = Session()
result = session.query(Zakaznik_adresa).filter(Zakaznik_adresa.trvale_bydliste == "N").delete()
session.commit()

session = Session()
session.query(Zakaznik_adresa).filter(Zakaznik_adresa.mesto == 'Cityville').update({"ulice": 'Nová ulice'})
result = session.query(Zakaznik_adresa).filter(Zakaznik_adresa.ulice == 'Nova ulice').all()
session.commit()
session = Session()
result1 = session.query(Zakaznik_adresa).all()
session.commit()
for zakaznik in result1:
    print(zakaznik.mesto, zakaznik.ulice)
print()
result1 = session.query(Zakaznik_adresa).all()