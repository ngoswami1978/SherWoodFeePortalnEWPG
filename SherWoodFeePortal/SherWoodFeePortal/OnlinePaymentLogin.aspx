<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" EnableEventValidation="true"  CodeBehind="OnlinePaymentLogin.aspx.cs" Inherits="SherWoodFeePortal.OnlinePaymentLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SherWood Online Payment::</title>
     <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
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
    <script>
        

        $(document).ready(function () {
           
        });


        function CheckRollNo() {
            if ($("#txtRollno").val().trim() == '') {
                addAlert('Please enter the student roll no');
                $("#txtRollno").focus();
                return false;
            }
            else {
                
                if ($("#btnSubmit").val() == "Secure Login") {
                    $.ajax({
                        type: "POST",
                        async: true,
                        url: 'OnlinePaymentLogin.aspx/CheckRollNo',
                        data: '{Rollno: "' + $("#txtRollno").val().trim() + '" }',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            // debugger;
                            var data = JSON.parse(response.d);
                            if (data.Exist == "true") {
                                //$("#spnMsg").html('Username has already been taken');
                                //$("#spnMsg").removeClass("success").addClass("failure");
                                //$("#btnSubmit").prop('disabled', true);
                                RemoveAlert();
                                $("#txtRollno").html('');
                                $("#txtMobileNo").val(data.Mobno);
                                $("#hdnMobileNo").val(data.Mobno);
                                $("#hdnMailTemplate").val(data.MailTemplate);

                                document.getElementById("divMobile").style.display = "block";
                                document.getElementById("divMobile").style.visibility = "visible";

                                $("#btnSubmit").val('Send OTP');
                            }
                            else {
                                addAlert('Please enter the valid student roll no or roll no is not available in our system');
                                //$("#spnMsg").removeClass("failure").addClass("success");
                                $("#btnSubmit").prop('disabled', false);
                                return false;

                            }
                        }
                    });
                }
                // SEND OTP CODE
                if ($("#btnSubmit").val() == "Send OTP") {
                    //alert($("#btnSubmit").val());
                    document.getElementById("divOTP").style.display = "block";
                    document.getElementById("divOTP").style.visibility = "visible";

                    $("#btnSubmit").val('Login');
                   // $("#btnSubmit").prop('disabled', true);
                   
                    $.ajax({
                        type: "POST",
                        async: true,
                        url: "OnlinePaymentLogin.aspx/GetOTPResponse",
                        data: '{Mobno: "' + $("#txtMobileNo").val().trim() + '" }',
                        //data: '{}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var data = JSON.parse(response.d);
                            //alert(data.status);
                            //alert(data.batch_dtl.status);
                            if (data.status == "success") {
                                //$("#btnSubmit").prop('disabled', false);
                                $("#hdnOTP").val(data.OTP)
                                
                                //return false;
                            }
                            else {
                                //return false;
                            }
                        }
                    });

                    return false;
                } // END OF OTP CODE

                // Login into Portal
                if ($("#btnSubmit").val() == "Login") {
                    //alert($("#hdnOTP").val());
                    if ($("#txtOTP").val().trim() == '') {
                        addAlert('Please enter the one time password!');
                        $("#txtOTP").focus();
                        return false;
                    }
                    else {
                        //alert('yes');
                        //$('#btnSubmit').click();
                        if ($("#txtOTP").val().trim() == $("#hdnOTP").val().trim()) {
                            return true;
                        }
                        else {
                            addAlert('Please enter the Correct One time password!');
                            $("#txtOTP").val('');
                            $("#txtOTP").focus();
                            return false;
                        }


                    }
                } // END OF Login INto Portal Logic

                return true;
            }



           

            function OnSuccess(response) {
                //debugger;
                //alert(response.d);

                //var r = { "status": "success", "description": { "desc":"1 messages scheduled for delievery", "batchid": "18805655","batch_dtl": [{ "mobileno": "55", "msgid": "88057802", "status":"SENT"}]} };
                //alert(JSON.stringify(response.d));
                var data = JSON.parse(response.d);
                alert(data);
                alert(data[0].status);
                //alert(response.d[0]);
                $.each(data, function (idx, obj) {
                    //alert(obj.status);
                });

                //alert(data[0]);
                //alert(data.status);
                //alert(data.batchid);

            }


            function addAlert(message) {
                $('#alerts').html(
                        '<div class="alert alert-danger">' +
                         '<button type="button" class="close" data-dismiss="alert">' +
                         '&times;</button>' + message + '</div>');
            }

            function RemoveAlert() {
                $('#alerts').html('');
            }

            $('.alert .close').live("click", function (e) {
                $(this).parent().hide();
            });
        }
        

    
    </script>
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
                        <b style="color: #c7c7c7 !important">Welcome To Online Payment</b>
                        <asp:HiddenField ID="hdnOTP" runat="server" />
                        <asp:HiddenField ID="hdnMailTemplate" runat="server" />

                    </h3>
                    <asp:Label ID="lblError" runat="server" Style="color: red; background-color: white"></asp:Label>
                    <div id="alerts">
                    </div>
                    <asp:TextBox ID="txtRollno" runat="server" CssClass="form-control" placeholder="Student Roll No."
                        required="required"></asp:TextBox>
                    <br />

                    <div id="divMobile" style="display:none;visibility:hidden;">
                        <asp:TextBox ID="txtMobileNo" Enabled="false" ReadOnly="true" runat="server" CssClass="form-control" placeholder="Registered mobile no"
                            required="required"></asp:TextBox>
                        <asp:HiddenField ID="hdnMobileNo" runat="server" />
                    </div>

                     <br />  
                     <div id="divOTP" style="display:none;visibility:hidden;">
                        <asp:TextBox ID="txtOTP" runat="server" CssClass="form-control" placeholder="Enter one time password"
                            required="required"></asp:TextBox>
                    </div>
                <br />  
               <%-- <asp:Button ID="btnSubmit" runat="server" 
                        CssClass="btn btn-lg btn-primary btn-block" Text="Secure Login" 
                        onclick="btnSubmit_Click" OnClientClick="CheckRollNo();" />--%>

                        <asp:Button ID="hdnButton" runat="server" Text="" Visible="False" 
            ClientIDMode="Static" OnClick="btnSubmit_Click" />
                    <asp:Button id="btnSubmit" runat="server" Text="Secure Login"  OnClick="btnSubmit_Click" CssClass="btn btn-lg btn-primary btn-block" type="button" value="Secure Login"
                        OnClientClick="CheckRollNo();" />
                </div>
              
              
            </div>
        </div>
    </div>
    </form>
</body>
</html>
