<?xml version="1.0" encoding="UTF-8"?>

<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<xsl:template match="/">
  <html>
  <body>
  <h2>Studenti</h2>
  <table border="1">
    <tr bgcolor="#9acd32">
      <th>Jmeno</th>
      <th>Prijmeni</th>
    </tr>
    <xsl:for-each select="university/fakulta/studenti/student">
    <tr>
      <td><xsl:value-of select="jmeno"/></td>
      <td><xsl:value-of select="prijmeni"/></td>
    </tr>
    </xsl:for-each>
  </table>
  </body>
  </html>
</xsl:template>

</xsl:stylesheet>