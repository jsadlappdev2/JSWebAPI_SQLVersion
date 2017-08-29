<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test2.aspx.cs" Inherits="JSWebAPI_SQLVersion.WebForms.TestingFunction.test2" %>
<HTML>
<SCRIPT language="javascript">
function animate(wc)
{
	var o = document.getElementById('requestor');
	o.src ="http://www.eon.com.hk/common/fcg/estroke.fcg?task=getUserPhrase&phrase="+wc;
}
</SCRIPT>

<BODY>
Click on the following Chinese Characters to animate<BR>
<a href="Javascript:animate('0x6211')">&#25105</A>
<a href="Javascript:animate('0x66130x59820x53cd0x638c')">&#26131;&#22914;&#21453;&#25484;</a><P>

<IFRAME  name=estroke SRC="http://www.eon.com.hk/common/fcg/estroke.fcg?task=getPage&&page=demo/template2.html&&bgcolor=ffffff&&bias=Simplified&&plan=Nine Square&&transientColor=ff0000&&strokeColor=000000&&radicalColor=0000ff&&planColor=00ff00&&appsize=200&noinput=true" frameborder=0 scrolling=no WIDTH=350 HEIGHT=256></IFRAME>


<IFRAME id=requestor WIDTH=0 HEIGHT=0 frameborder=0 scrolling=no></IFRAME>

</HTML>

