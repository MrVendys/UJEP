<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:param name="kodPredmetu" />
  <xsl:template match="/">
    <html>
      <body>
         <xsl:apply-templates select="//predmet[@kod=$kodPredmetu]" />
      </body>
    </html>
  </xsl:template>

  <xsl:template match="predmet">
  <table border="1">
    <!-- Select all predmet elements -->
    <xsl:if test="@kod = $kodPredmetu">
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
          <!-- Print kod/nazev -->
          <xsl:value-of select="@kod"/>/<xsl:value-of select="nazev"/><br/>
          <!-- Print additional details -->
          <xsl:for-each select="vyucujici">
            <xsl:value-of select="jmeno"/><br/>
            <xsl:value-of select="email"/><br/>
            <xsl:value-of select="telefon"/><br/>
          </xsl:for-each>
          Kredity: <xsl:value-of select="kredity"/><br/>
          Status: <xsl:value-of select="status"/><br/>
          Zakonceni: <xsl:value-of select="zakonceni"/><br/>
        </td>
      </tr>
    </xsl:if>
  </table>
</xsl:template>

</xsl:stylesheet>
