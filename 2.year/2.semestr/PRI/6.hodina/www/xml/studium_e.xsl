<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:template match="/">
    <html>
      <body>
        <xsl:apply-templates select="/studium"/>
      </body>
    </html>
  </xsl:template>

  <xsl:template match="studium">
    <table border="1">
      <xsl:for-each select="//rocnik[3]/semestr[last()]/predmet">
        <xsl:sort select="kredity" data-type="number" order="ascending"/>
        <tr>
          <td><xsl:value-of select="@kod"/></td>
          <td><xsl:value-of select="nazev"/></td>
          <td><xsl:value-of select="kredity"/></td>
        </tr>
      </xsl:for-each>
    </table>
    <p>Celkem kreditních bodů = <xsl:value-of select="sum(//rocnik[3]/semestr[last()]/predmet/kredity)"/></p>
  </xsl:template>

</xsl:stylesheet>