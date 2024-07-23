
## 📜 Popis projektu

V tomto projektu bylo cílem seznámit se s návrhovými vzory pomocí jazyka "C#".
Za úkol bylo vytvořit projekt s využitím alespoň 3 návrhových vzorů
Já jsem využil "Factory", "States", "Strategy", "Singleton"

## ⚙️ Funkce

- Projekt je jednoduchá konzolová aplikace na téma kavárny
- V kavárně (Singleton) pracuje "barista", který využívá vzor "States"
- Pomocí jednotlivých "States" jsou implementovány další vzory "Factory" a "Strategy"
  - "Čeká" na příchod zákazníka
  - "Dělá kávu" ("Factory")
  - "Sprocesuje typ platby" ("Strategy")
- Dělání kávy je řešeno přes vzor "Factory"
  - Jednotlivé kávy mají svůj vlastní "kávovar" (Factory)
  - "Espresso" -> "EspressoMachine"
  - "CaffeLatte" -> "CaffeLatteMachine"
  - ...
- Na typ platby je využit vzor "Strategy"
  - Jednotlivé možnosti placení dědí "IPaymentStrategy" a přispůsobují si ho podle sebe
  - "PaymentByBank"
  - "PaymentByCash"
  - "PaymentByCreditCard"
  
## 🧠 Použité techniky

- Návrhové vzory
- Dědění
- Konzolová aplikace
- Zapisování do souboru (User/Documents/Menu.txt, když tak si ho potom smažte)

## 🎮 Ovládání
- Po spuštění se objeví konzole s hláškou od baristy.
- Komunikace je skrze psaní do konzole, většinou pomocí příkazů vyobrazených v konzoly.
- Pro příchod do kavárny zmáčkněte "Enter"
- Barista se zeptá na nápoj
- Seznam nápojů si můžete zobrazit příkazem "menu"
- Nápoj vyberete zadáním čísla u něho
- Poté následuje vybrání typu placení - zase pomocí zadání čísla

## 📂 Struktura projektu

- **Cafe.sln**: Hlavní řešení projektu.
- **Program.cs**: Hlavní vstupní bod aplikace.
- **CafeShop.cs**: Sekundární vstupní bod aplikace.
- **CafeWorker.cs**: Implementace vzoru "States"
- **📂Factory**: Řešení  vzoru "Factory"
- **📂States**: Řešení vzoru "States"
  - implementace "Strategy" a "Factory"
- **📂Strategy**: Řešení vzoru "Strategy"
- **Menu.cs**: Zapisování a čtení souboru.

## 🔧 Požadavky

- .NET Framework 4.7.2 nebo vyšší
- Visual Studio 2019 nebo novější

## 🛠️ Instalace
### .exe souboru
- V této složce soubor Cafe.exe
- Kliknout na něj
- Vpravo nahoře tlačítko "Download raw file"
### Celé řešení (kód)
- Vrátit se zpět na [repozitář UJEP](../)
- v README.md dole je návod na stáhnutí
