## 📜 Úvod

Repozitář obsahuje projekty z kurzu Programování pro internet a následně semestrální práci.
Cílem semestrální práce bylo setrojit webovou aplikaci za použití PHP a XML (XSD, XSL).
Využívat XML na ukládání a načítání dat. Validovat pomocí XSD a XSL.

## ⚙️ Funkce

- Vizualizace seznamu her.
- Upravoání uložené hry.
- Přídávání hry.
- Mazáni hry.
- Vyhledávání hry podle názvu.
- Správné ukládání u více platforem

## 🧠 Použité techniky

- PHP
- JavaScript
- XML
- XSD
- XSL
  
## 🎮 Ovládání
- Webová aplikace se ovládá myší
- Vpravo nahoře jsou prokliky na jednotlivé stránky:
  - **Game List**: Zobrazí stránku s tabulkou hre
  - **Search**: Zobrazí stránku na vyhledávání
    - Po vyhledání hry se zobrazí panel s hrou.
    - Dole jsou 2 tlačítka, na editování hry a smazání hry
  - **Add Game**: Nasměruje na stránku pro přidání hry.

## 📂 Struktura projektu

- **📂www/html**: Hlavní řešení projektu
  - **📂php**: Php scripty na přidání, odebrání a editování hry
  - **📂scripts**: Javascript scripty na transformaci xml souboru do html
  - **📂xml**: Xml soubory pro ukládání her
- **add_game.html**: Html stránka na přidání hry do xml souboru
- **edit_game.htlm**: Html stránka na editování hry
- **games.html**: Html stránka na zobrazení her z xml souboru
- **index.html**: Úvodní stránka
- **search.html**: Stránka na vyhledávání hry

## 🔧 Požadavky
- Visual Studio Code
- [Docker](https://www.docker.com/products/docker-desktop/)

## 🛠️ Instalace
### .exe souboru
- Tento projekt neobsahuje .exe soubor
- Projekt běží na lokálním webu a jde spusti jedině přes běžící kód (viz níže)

### Celé řešení (kód)
- K spuštění je třeba nainstalovat Visual Studio Code (Nebo jiné IDE) a Docker
- Vrátit se zpět na [repozitář UJEP](../)
- Stáhnout repozitář
- Spusti Docker
- Otevřít si PRI složku ve Visual Code (Případně jiné IDE)
- Otevřít si terminál (Buď přímo v počítači nebo ve Visual studiu Code -> Vlevo nahoře **Terminal** -> **New Terminal**
- V terminálu se dosta do složky "Seminarka"
```
cd seminarka
```
Spustit aplikaci přes Docker
```
docker-compose up
```

Webová aplikace běží na adrese localhost:8000
## 📸 Ukázka

https://github.com/user-attachments/assets/122fa4ba-1c47-470b-91af-61454177eeea

