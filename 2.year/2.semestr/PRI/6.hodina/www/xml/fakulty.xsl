<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:template match="/">
        <html>
            <head>
                <style>
                    table {
                        width: 100%;
                        border-collapse: collapse;
                    }
                    th, td {
                        border: 1px solid black;
                        padding: 8px;
                        text-align: left;
                        width:25%;
                    }
                    th {
                        background-color: #f2f2f2;
                    }
                    .name{
                        font-weight: bold;
                        font-size: 28px;
                    }
                    .dean{
                        color: gray;
                        font-style: italic;
                        font-size: 14px;
                    }
                    .department-name{
                        font-size: 24px;
                    }
                    .departments{
                        margin: 20px 70px 0px 70px;
                    }
                </style>
            </head>
            <body>
                <xsl:apply-templates select="faculties/faculty"/>
            </body>
        </html>
    </xsl:template>

    <xsl:template match="faculty">
        <hr/>
        <div class="name">
            <xsl:value-of select="name"/>
        </div>
        <div class="dean">
            <xsl:value-of select="dean"/>
        </div>
        <div class="departments">
            <xsl:apply-templates select="departments/department"/>
        </div>
    </xsl:template>

    <xsl:template match="department">
        <div class="department-name">
            <xsl:value-of select="name"/>
        </div>
        <div class="dean">
            <xsl:value-of select="head"/>
        </div>
        <h3>Employees</h3>
        <table>
            <tr>
                <th>Name</th>
                <th>Contact</th>
                <th>Position</th>
                <th>Titles</th>
            </tr>
            <xsl:apply-templates select="employees/employee"/>
        </table>
        <br/>
    </xsl:template>

    <xsl:template match="employee">
        <tr>
            <td>
                <xsl:value-of select="name"/>
            </td>
            <td>
                <xsl:value-of select="contact"/>
            </td>
            <td>
                <xsl:value-of select="position"/>
            </td>
            <td>
                <xsl:for-each select="titles/title">
                    <xsl:value-of select="."/>
                    <xsl:if test="position() != last()">, </xsl:if>
                </xsl:for-each>
            </td>
        </tr>
    </xsl:template>

</xsl:stylesheet>