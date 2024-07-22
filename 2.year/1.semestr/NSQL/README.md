

## 📜 Popis projektu

Tento projekt byl vytvořen v rámci kurzu NoSQL Databázové systémy. Za úkol bylo vytvořit webovou aplikaci v pythonu s využíváním některé NoSQL databáze.
Pro náš projekt jsme si vybrali databázový systém MongoDB

Obsah projektu spočívá v jednoduchém záznamníku inspirovaném hrou Dungeons & Dragons.


## ⚙️ Funkce

- Login (testovací uživatelé: "bao" a "venca")
- Logout
- Přidávání spellů
- Mazání spellů
  

## 🧠 Použité techniky

- Docker
- MongoDB
- Flask

## 🎮 Ovládání
- Klikání na jednotlivé záložky přepínáte zobrazované stránky
- viz video
## 📂 Struktura projektu
- **📂App**: Řešení celé aplikace
- **📂code**: Kód
- **📂static**: Grafika aplikace pomocí css a bootstrapu
- **app.py**: Hlavní vstup aplikace
- **data.py**: Předvytvořená data
- **db.py**: Vytvoření a napojení na databázi
- 
## 🔧 Požadavky
- Python (3.11)
- [Docker](https://www.docker.com/products/docker-desktop/)

## 🛠️ Instalace
- Pro spuštění je potřeba nainstalovat Docker (pro běh databáze)
### .exe souboru
- Tento projekt neobsahuje .exe soubor
- Spustit lze jen přes kód

### Celé řešení (kód)
- Vrátit se zpět na [repozitář UJEP](../)
- Stáhnout repozitář
- Otevřít si NSQL složku ve Visual Code (Případně jiné IDE)
- Vytvořit si v terminálu virtuální prostředí (venv) na nainstalování knihoven (neni potřeba, ale doporučeno. Jinak se budou stahovat přímo do počítače)
```
  pip -m venv venv
```
Aktivovat
- Musíte být ve stejném adresáři, jako je vytvořený venv.. jinak tato cesta nebude funovat
```
  venv\script\activate
```
Stáhnutí knihoven
```
pip install requirements.txt
```
Zapnutí aplikace přes Docker
```
cd app
docker-compose up
```
Webová aplikace běží na adrese localhost:5000
## 📸 Ukázka simulace

![Video NSQL](https://github.com/user-attachments/assets/16a8bce2-77b5-41dc-9f3a-73f62a4e5258)

