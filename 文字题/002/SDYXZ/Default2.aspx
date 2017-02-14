<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <style>


   .demo-browser {
    /*background-image: url(../img/demo/browser-2x.png);*/
   background-color: rgb(230, 189, 189);
   text-align:center
  }

  .demo-browser-side {
  float: left;
  padding: 22px 20px;
  width: 151px;
}
 table img {
  border: 2px solid #ffffff;
  margin: 0 15px 20px 0;
  width: 50px;
  height:50px;
}
  .demo-browser-content {
  overflow: hidden;
  padding: 21px 0 0 20px;
  border-radius: 0 0 6px;
  margin-left:400px;
  margin-top:30px;
}
         h1
         {
             float: left;
             margin: 0 15px 20px 0;
             width: 146px;
         }
         a:hover {
  color: #0a0a0a;
  text-shadow: 
    /* Color 1 */
    1px 1px #61b4de,
    /* Color 2 */
    2px 2px #91c467,
    /* Color 3 */
    3px 3px #f3a14b,
    /* Color 4 */
    4px 4px #e84c50,
    /* Color 5 */
    5px 5px #4e5965;
}
a {
  color: #ededed;
  font-family: Oswald;
  font-size: 4em;
  transition: 0.5s all ease;
  text-transform: uppercase;
  text-decoration: none;
  
  padding-bottom:300px;
  text-shadow: 
    /* Color 1 */
    1px 1px #61b4de, 2px 2px #61b4de, 3px 3px #61b4de, 4px 4px #61b4de, 5px 5px #61b4de,
    /* Color 2 */
    6px 6px #91c467, 7px 7px #91c467, 8px 8px #91c467, 9px 9px #91c467, 10px 10px #91c467,
    /* Color 3 */
    11px 11px #f3a14b, 12px 12px #f3a14b, 13px 13px #f3a14b, 14px 14px #f3a14b, 15px 15px #f3a14b,
    /* Color 4 */
    16px 16px #e84c50, 17px 17px #e84c50, 18px 18px #e84c50, 19px 19px #e84c50, 20px 20px #e84c50,
    /* Color 5 */
    21px 21px #4e5965, 22px 22px #4e5965, 23px 23px #4e5965, 24px 24px #4e5965, 25px 25px #4e5965;
}


/* Border styles */
#table-1 thead, #table-1 tr {
border-top-width: 1px;
border-top-style: solid;
border-top-color: rgb(230, 189, 189);
}
#table-1 {
border-bottom-width: 1px;
border-bottom-style: solid;
border-bottom-color: rgb(230, 189, 189);
border-spacing:0px;
}

/* Padding and font style */
#table-1 td, #table-1 th {
padding: 5px 10px;
font-size: 12px;
font-family: Verdana;
color: rgb(177, 106, 104);
    border: 1px solid #ddd;
}

/* Alternating background colors */
#table-1 tr:nth-child(even) {
background: rgb(238, 211, 210)
}
#table-1 tr:nth-child(odd) {
background: #FFF
}


    </style>
</head>
<body>
    <form id="form1" runat="server">
         <div  class="demo-browser">
             <a href="#">射雕英雄传(第一回　风雪惊变)</a>

            <div id="divInfo" runat="server" class="demo-browser-content">
                 <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" width="900px" Height="500px" style="display:none"></asp:TextBox>
            </div>
          </div>
    </form>
</body>
</html>
