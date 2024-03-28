<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:template match="/">
    <html>
      <body>
        <xsl:apply-templates select="/studium"/>
      </body>
    </html>
  </xsl:template>

  <xsl:template match="/studium">
  <table border="1">
    <xsl:for-each select="//predmet">
      <tr>
        <td>
          <xsl:attribute name="style">
            <xsl:choose>
              <xsl:when test="@katedra='KI'">background-color: #FFDDDD;</xsl:when>
              <xsl:when test="@katedra='KMA'">background-color: #DDFFDD;</xsl:when>
              <xsl:when test="@katedra='CJP'">background-color: #DDDDFF;</xsl:when>
              <!-- Add more conditions for other katedra values -->
              <xsl:otherwise>background-color: #FFFFFF;</xsl:otherwise>
            </xsl:choose>
          </xsl:attribute>
          <xsl:value-of select="@kod"/>/<xsl:value-of select="nazev"/>
        </td>
      </tr>
    </xsl:for-each>
  </table>
</xsl:template>

</xsl:stylesheet>
