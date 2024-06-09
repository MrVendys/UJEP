<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  
  <!-- Match root element -->
  <xsl:template match="/studium">
    <html>
      <head>
        <title>Studium</title>
      </head>
      <body>
        <h1>Studium</h1>
        <xsl:apply-templates select="rocnik"/>
      </body>
    </html>
  </xsl:template>
  
  <xsl:template match="rocnik">
    <h2>Rocnik <xsl:value-of select="@cislo"/></h2>
    <xsl:apply-templates select="semestr"/>
  </xsl:template>
  
  <xsl:template match="semestr">
    <h3>Semestr: <xsl:value-of select="@nazev"/></h3>
    <table border="1">
      <tr>
        <th>Predmet</th>
        <th>Vyucujici</th>
        <th>Kredity</th>
        <th>Status</th>
        <th>Zakonceni</th>
      </tr>
      <xsl:apply-templates select="predmet"/>
    </table>
  </xsl:template>
  
  <xsl:template match="predmet">
    <tr>
      <td><xsl:value-of select="nazev"/></td>
      <td>
        <xsl:value-of select="vyucujici/jmeno"/><br/>
        <xsl:value-of select="vyucujici/email"/><br/>
        <xsl:value-of select="vyucujici/telefon"/>
      </td>
      <td><xsl:value-of select="kredity"/></td>
      <td><xsl:value-of select="status"/></td>
      <td><xsl:value-of select="zakonceni"/></td>
    </tr>
  </xsl:template>
  
</xsl:stylesheet>
