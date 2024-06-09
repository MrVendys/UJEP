<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

    <xsl:template match="/">
        <select name="kod_predmetu">
            <xsl:for-each select="//predmet">
                <option value="{@kod}">
                    <xsl:value-of select="@kod" /> - <xsl:value-of select="nazev" />
                </option>
            </xsl:for-each>
        </select>
    </xsl:template>

</xsl:stylesheet>