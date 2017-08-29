<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chinesestroke.aspx.cs" Inherits="JSWebAPI_SQLVersion.WebForms.TestingFunction.chinesestroke" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
   
        Chinese character stroke:<br />
        <br />
   
    </div>
        <div>
        <IFRAME SRC="http://www.eon.com.hk/common/fcg/estroke.fcg?task=getPage&page=demo/estrokeAdvert.html&bgcolor=ffffff&bias=Simplified&plan=Nine Square&transientColor=ff0000&strokeColor=000000&radicalColor=0000ff&planColor=00ff00&appsize=200" frameborder=0 scrolling=no WIDTH=350 HEIGHT=256></IFRAME> 
</div>
        <div>
            <IFRAME SRC="http://www.eon.com.hk/common/fcg/estroke.fcg?task=getPage&page=demo/matchGame.html&bgcolor=ffffff" frameborder=0 scrolling=no WIDTH=480 HEIGHT=480></IFRAME> 

        </div>
    </form>
</body>
</html>
