<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

    <xsl:template match="/">
        <html>
            <head>
            </head>
            <body>
                <table border="1">
                    <tr>
                        <th>Title</th>
                        <th>Developer</th>
                        <th>Publisher</th>
                        <th>Platform</th>
                        <th>Release Date</th>
                        <th>Genre</th>
                    </tr>
                    <xsl:for-each select="games/game">
                        <tr>
                            <td><xsl:value-of select="title"/></td>
                            <td><xsl:value-of select="developer"/></td>
                            <td><xsl:value-of select="publisher"/></td>
                            <td><xsl:value-of select="platform"/></td>
                            <td><xsl:value-of select="release_date"/></td>
                            <td><xsl:value-of select="genre"/></td>
                        </tr>
                    </xsl:for-each>
                </table>
            </body>
        </html>
    </xsl:template>

</xsl:stylesheet>
