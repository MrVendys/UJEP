# Vrhcáby (Backgammon)

## 📜 Popis projektu

Tento projekt je implementací populární deskové hry **Vrhcáby (Backgammon)** v jazyce **Python**. 
Cílem hry je dostat své "kameny" přes celou hrací desku až do svého "domečku" dříve, než protihráč. Na cestě může přitížit protihráči tak, že vyhodí jeho kámen z pole až na začátek hrací desky (na tzv. "bar"). Kdo jako první přesune všechny své kameny do domečku (pryč z hrací desky), vyhrává.

***Jednoduchá pravidla***
- Hráč má k dispozic 15 kamenů své barvy.
- Ve svém tahu hodí dvěmi kostkami a podle výsledku hodu může posunout svým kamenem či kameny o příslučný počet polí k cíli.
- Např. Hráč hodí na kostkách 2 a 5. Ve svém kole se může pohybovat buď:
  - Jedním kamenem o 2 pole a následně o dalčích 5 polí (Pokud splňuje další pravidla o obsazeném poli)
  - Jedním kamenem o 2 polě a druhým kamenem o 5 polí
- Na jednom poli může být maximálně 5 kamenů stejné barvy. Pokud se stane, že musí hráč posunout svůj kámen na pole se soupeřovým **jedním** kamenem -> vyhodí ho zpátky na začátek
- Hráč nemůže přesunout svůj kámen na pole, kde soupeř má **2 a více** kamenů
- Jakmile hráč přesune všechny svoje kameny na poslední 1/4 hrací desky, až nyní může přesouvat kameny do domečku. Přesouvání probíhá pomocí normálního pohybu kamenem pomocí hodu dvěmi kostkami.

## ⚙️ Funkce

- Plné grafické prostředí.
- Pohyb kamenů
- Grafické znázornění vybraného kamene a polí, na které se může phnout
- Detekce plných polí
- Automatické házení kostkou
- Jednoduchý naprogramovaný protihráč
- Vyhazování kamenů

## 🧠 Použité techniky

- Pygames
- Třídy
  
## 🎮 Ovládání
- Po spuštění se ihned načte hra s hráčem na řadě.
- Kostky už jsou hozeny a hráč může začít hrát
- Kliknutím na nějaká svůj kámen se kámen označí, zároveň se označí i pole, kam s kamenem může hráč posunout podle hodu na kostkách
- Na polích, kde má hráč více kamenů, může označit jen ten první (nejvyšší)
- Po posunu začne hrát počítač
- Pokud dojde k vyhození kamenu, hráč, kterýmu byl kámen vyhozen, ve svém dalším tahu musí nejdřív odehrát s vyhozeným kamenem.
- Např. Počítač mi vyhodí na hracím poli jeden kámen, jsem na tahu a kámen se mi objeví v mém baru uprostřed hrací plochy. Musím nejdřív nasadit tento kámen, než budu moci hrát s ostatními.
- 

## 📂 Struktura projektu

- **2048.sln**: Hlavní řešení projektu.
- **Program.cs**: Hlavní vstupní bod aplikace.
- **Form1.cs**: Logika i grafické rozhraní pomocí Windows Forms.
- **Cell.cs**: Třída typu User control pro jednotlivou destičku
- **GameOver.cs** Třída typu Windows Forms. Okno konce hry.

## 🔧 Požadavky

- .NET Framework 4.7.2 nebo vyšší
- Visual Studio 2019 nebo novější

## 🛠️ Instalace
### .exe souboru
- V této složce soubor 2048.exe
- Kliknout na něj
- Vpravo nahoře tlačítko "Download raw file"
### Celé řešení
- Vrátit se zpět na [repozitář SPSUL](../)

## 📸 Ukázka hry

![Screenshot hry 2048](2048_screenshot.png)

