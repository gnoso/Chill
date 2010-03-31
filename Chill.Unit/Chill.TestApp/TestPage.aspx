<%@ Page Language="C#" %>
<%@ Import Namespace="System.IO"%>
<html>
    <head>
        <title>NCover.CassiniLib Says Hello World!</title>
    </head>
    <body>
        <h1>Hello World!</h1>
        
        <p><%
               Response.Write(Request.Form["test"]); %></p>
               
        <% Response.Write(new StreamReader(Request.InputStream).ReadToEnd()); %>
        
        <form method="post" action="TestPage.aspx">
            <input type="text" name="test" />
            <input type="submit" />
        </form>
    </body>
</html>
