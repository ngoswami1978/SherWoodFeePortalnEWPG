<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="SherWoodFeePortal.Logout" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SherWood Online Payment:: Logout</title>
     <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">

     <!-- Bootstrap Core CSS -->
    <link href="vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">

    <!-- Theme CSS -->
    <link href="css/freelancer.min.css" rel="stylesheet">

    <!-- Custom Fonts -->
    <link href="vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/css?family=Montserrat:400,700" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/css?family=Lato:400,700,400italic,700italic" rel="stylesheet" type="text/css">

    <style>
        
         body, html {
        height: 100%;
        background-repeat: no-repeat;
        background-image: linear-gradient(rgb(104, 145, 162), rgb(0, 85, 49));
    }

    .wrapper {
        margin-top: 100px;
        margin-bottom: 20px;
    }

    .form-signin {
        max-width: 420px;
        padding: 10px 38px 66px;
        margin: 0 auto;
        background-color: #005531;
        border: 3px dotted rgba(0,0,0,0.1);
        border-radius: 5px;
    }

    .form-signin-heading {
        text-align: center;
        margin-bottom: 20px;
    }

    .form-control {
        position: relative;
        font-size: 16px;
        height: auto;
        padding: 10px;
    }

    input[type="text"] {
        margin-bottom: 0px;
        border-bottom-left-radius: 0;
        border-bottom-right-radius: 0;
    }

    input[type="password"] {
        margin-bottom: 20px;
        border-top-left-radius: 0;
        border-top-right-radius: 0;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="container">
            <div class="wrapper">
                <div class="form-signin">
                    <h3 class="form-signin-heading">
                        <img src="../img/logo5.png" alt="Alternate Text" />
                        <br />
                        <b style="color: #c7c7c7 !important">Your session has been expired!</b>
                    </h3>
                   
                    <asp:Label ID="lblMessage" runat="server" style="color:white">
                    <center><b>Please login </b><a href="onlinePaymentlogin.aspx">again</a></center>
                    </asp:Label>
                    
                 
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>