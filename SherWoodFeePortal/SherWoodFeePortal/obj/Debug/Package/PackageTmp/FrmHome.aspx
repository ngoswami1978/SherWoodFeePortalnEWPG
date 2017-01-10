<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="FrmHome.aspx.cs"
    Inherits="SherWoodFeePortal.FrmHome" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SherWood Online Payment::</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">
    <!-- jQuery -->
    <script src="vendor/jquery/jquery.min.js"></script>
    <script src="vendor/bootstrap/js/bootstrap.min.js"></script>
    <!-- jQuery -->
    <script src="vendor/jquery/jquery.min.js"></script>
    <script src="vendor/bootstrap/js/bootstrap.min.js"></script>

    <%--<script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>--%>
    <!-- Bootstrap Core CSS -->
    <link href="css/bootstrap.css" rel="stylesheet" />
    <%--<link href="css/bootstrap-theme.css" rel="stylesheet" />--%>
    <link href="css/freelancer.css" rel="stylesheet" />
    <%--<link href="css/bootstrap.css" rel="stylesheet" />--%>
    <link href="Styles/Stepwizard.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Stepwizard.js" type="text/javascript"></script>
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/freelancer.css" rel="stylesheet" />
    <link href="Styles/Stepwizard.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Stepwizard.js" type="text/javascript"></script>

    <style type="text/css">
        #loading-div-background
        {
            display: none;
            position: fixed;
            top: 0;
            left: 0;
            background: black;
            width: 100%;
            height: 100%;
        }
        
        #loading-div
        {
            border-radius: 25px;
            width: 300px;
            height: 200px;
            background-color: #0c0b0b;
            text-align: center;
            position: absolute;
            left: 50%;
            top: 50%;
            margin-left: -150px;
            margin-top: -100px;
            opacity: 1;
        }
    </style>

    <style type="text/css">
        #loading-div-pleasewait
        {
            display: none;
            position: fixed;
            top: 0;
            left: 0;
            background: black;
            width: 100%;
            height: 100%;
        }
        
        #loading-pleasewait-div
        {
            border-radius: 25px;
            width: 300px;
            height: 200px;
            background-color: #0c0b0b;
            text-align: center;
            position: absolute;
            left: 50%;
            top: 50%;
            margin-left: -150px;
            margin-top: -100px;
            opacity: 1;
        }
    </style>

    <style type="text/css">
        table, td, th
        {
            border: 1px solid #ddd;
            text-align: left;
        }
        
        table
        {
            border-collapse: collapse;
            width: 100%;
        }
        
        th, td
        {
            padding: 15px;
        }
        
        .bar-container
        {
            display: table-cell;
            width: 50%;
        }
        .bar
        {
            display: inline-block;
            vertical-align: top;
            width: 100%;
            height: 50%;
            border-bottom: 1px solid lightGray;
        }
        .text
        {
            display: table-cell;
            padding-left: 5px;
            padding-right: 5px;
        }
    </style>

    <style type="text/css">
        table, td, th
        {
            border: 1px solid #ddd;
            text-align: left;
        }
        
        table
        {
            border-collapse: collapse;
            width: 100%;
        }
        
        th, td
        {
            padding: 15px;
        }
        
        .bar-container
        {
            display: table-cell;
            width: 50%;
        }
        .bar
        {
            display: inline-block;
            vertical-align: top;
            width: 100%;
            height: 50%;
            border-bottom: 1px solid lightGray;
        }
        .text
        {
            display: table-cell;
            padding-left: 5px;
            padding-right: 5px;
        }
        .blink
        {
            text-decoration: blink;
        }
        #errmsg
        {
            color: red;
        }
    </style>

    <style type="text/css">
        /* When the body has the loading class, we turn
       the scrollbar off with overflow:hidden */
        body.loading
        {
            overflow: hidden;
        }
        
        /* Anytime the body has the loading class, our
       modal element will be visible */
        body.loading .modal
        {
            display: block;
        }
        
        div#spinner
        {
            display: none;
            width: 100px;
            height: 100px;
            position: fixed;
            top: 50%;
            left: 50%;
            background: url(spinner.gif) no-repeat center #fff;
            text-align: center;
            padding: 10px;
            font: normal 16px Tahoma, Geneva, sans-serif;
            border: 1px solid #666;
            margin-left: -50px;
            margin-top: -50px;
            z-index: 2;
            overflow: auto;
        }
    </style>

    <%--<script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>--%>
    <script src="vendor/jquery/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/blitzer/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        function noBack() {
            window.history.forward()
        }
        noBack();
        window.onload = noBack;
        window.onpageshow = function (evt) { if (evt.persisted) noBack(); }
        window.onunload = function () { void (0); }


        $(document).ready(function () {

            $(".clsClose a").live('click', function (e) {
                $('#loading-div-background').css("display", "none");
            });

            $("#loading-div-background").css({ opacity: 0.7 });
            $("#loading-div-pleasewait").css({ opacity: 0.5 });

            //called when key is pressed in textbox
            $("#NetPayAmount").keypress(function (e) {

                var $field = $(this);
                // this is the value before the keypress
                var beforeVal = $field.val();

                //if the letter is not digit then display error and don't type anything
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    //display error message
                    $("#errmsg").html("Digits Only").show().fadeOut("slow");
                    return false;
                }
                else {
                    setTimeout(function () {

                        // this is the value after the keypress
                        var afterVal = $field.val();
                        $("#amount").val(afterVal);
                    }, 0);
                }
            });
        });

        $(function () {
            $("#lnkBill").click(function () {
                $('#loading-div-background').css("display", "block");
            });
        });

        $(function () {
            $("#btnPayment").click(function () {
                $('#loading-div-pleasewait').css("display", "block");
            });
        });

        var spinnerVisible = false;
        function showProgress() {
            if (!spinnerVisible) {
                $("div#spinner").fadeIn("fast");
                spinnerVisible = true;
            }
        };
        function hideProgress() {
            if (spinnerVisible) {
                var spinner = $("div#spinner");
                spinner.stop();
                spinner.fadeOut("fast");
                spinnerVisible = false;
            }
        };

        $(function () {
            blinkeffect('#lblDueDate');
        })
        function blinkeffect(selector) {
            $(selector).fadeOut('slow', function () {
                $(this).fadeIn('slow', function () {
                    blinkeffect(this);
                });
            });
        }       
    </script>
</head>
<body>
    <form id="form1" runat="server" method="post">
    <%--<div id="loading">
    <p><img src="img/loading45.gif" /> Please Wait</p>
    </div>--%>
    <div id="spinner">
        Loading...
    </div>
    <!-- Navigation -->
    <nav id="mainNav" class="navbar navbar-default navbar-fixed-top navbar-custom">
        <div class="container">
            <!-- Brand and toggle get grouped for better mobile display -->
            <div class="navbar-header page-scroll">
                <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
                    <span class="sr-only">Toggle navigation</span> Menu <i class="fa fa-bars"></i>
                </button>
                <a class="navbar-brand" href="#page-top"><img src="../img/logo5.png" /></a>
            </div>

            <!-- Collect the nav links, forms, and other content for toggling -->
          <%--  <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                <ul class="nav navbar-nav navbar-right">
                    <li class="hidden">
                        <a href="#page-top"></a>
                    </li>
                 <li> <a href="#">My Account </a></li>
                    <li class="page-scroll">
                        <a href="#about">About</a>
                    </li>
                    <li class="page-scroll">
                        <a href="#contact">Contact</a>
                    </li>

                   
                    <li> <a href="#">Log Out </a></li>

                    <li class="login"> <a href="#">Welcome <asp:Label ID="lblName" runat="server" Text=""></asp:Label> </a></li>
                </ul>
                
            </div>--%>
            <!-- /.navbar-collapse -->
        </div>
        <!-- /.container-fluid -->
    </nav>
    <!-- Header -->
    <header>
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                   <%-- <img class="img-responsive" src="img/profile.png" alt="">--%>
                    <div class="intro-text">
                        <span class="skills">WELCOME TO SHERWOOD ONLINE PAYMENT (2017)</span>
                        <hr class="star-light">
                        <%--<span class="skills"></span>--%>
                    </div>
                </div>
            </div>
        </div>
    </header>
    <div class="page">
        <div class="main">
            <div class="container">
                <div id="frmError" runat="server">
                    <center>
                        <span style="color: red">Please fill all mandatory fields.</span></center>
                    <br />
                    <br />
                </div>
                <input type="hidden" runat="server" id="key" name="key" />
                <input type="hidden" runat="server" id="hash" name="hash" />
                <input type="hidden" runat="server" id="txnid" name="txnid" />
                <input type="hidden" runat="server" id="firstname" name="firstname" />
                <input type="hidden" runat="server" id="productinfo" name="productinfo" />
                <input type="hidden" runat="server" id="email" name="email" />
                <input type="hidden" runat="server" id="phone" name="phone" />
                <input type="hidden" runat="server" id="amount" name="amount" />
                <input type="hidden" runat="server" id="surl" name="surl" />
                <input type="hidden" runat="server" id="furl" name="furl" />
                <input type="hidden" runat="server" id="hdnBillrefno" name="furl" />
                <%--<asp:TextBox ID="service_provider" runat="server" Text="payu_paisa"/>--%>
                <div class="container">
                    <div class="row">
                        <div class="col-sm-3">
                            <h6>
                                <asp:Label ID="lblPeriod" runat="server" CssClass="control-label"></asp:Label>
                            </h6>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-3">
                            <h3>
                                Student Details</h3>
                        </div>
                        <div class="col-sm-2">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-5">
                            <h6>
                                <asp:Label ID="lblBillIssueDate" runat="server" CssClass="control-label"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblDueDate" runat="server" ForeColor="Red"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                <%--<asp:LinkButton ID="lnkBill" Enabled="false" Visible="false" runat="server" 
                                OnClick="lnkBill_Clicked"
                                    ForeColor="Green">DOWNLOAD BILL</asp:LinkButton>--%>
                                <%--<LinkButton ID="lnkBill" ForeColor="Green">DOWNLOAD BILL</LinkButton>--%>
                            </h6>
                        </div>
                        <input type="button" id="lnkBill" class="btn btn-success" value="DOWNLOAD BILL" />
                        <div id="loading-div-background" style="display: none">
                            <div id="loading-div" class="ui-corner-all" style="background: #005531;">
                                <div id="dialog" align="center">
                                    <div class="clsClose">
                                        <a href="#" id="Close" style="font-size: large; color: Black">Close me </a>
                                    </div>
                                    DOWNLOAD BILLS
                                    <asp:DropDownList runat="server" ID="DropDownListSelectBill" OnSelectedIndexChanged="DropDownListSelectBill_SelectedIndexChanged"
                                        AutoPostBack="true" OnTextChanged="DropDownListSelectBill_TextChanged" Height="30px"
                                        Width="250px">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div id="loading-div-pleasewait" style="display: none">
                            <div id="loading-pleasewait-div" class="ui-corner-all" style="background:white;" >
                                <img style="height: 80px; margin: 30px;" src="img/loading45.gif" alt="Loading.." />
                                <h2 style="color: gray; font-weight: normal;">
                                    Please wait....</h2>
                             </div>
                        </div>


                    </div>
                </div>
                <hr style="border-top: 1px double #8c8b8b" />
            </div>
            <div class="container">
                <asp:Panel ID="Panel" runat="server" Style="overflow-x: auto;">
                </asp:Panel>
            </div>
            <center>
                <asp:Label ID="lblOpeningBalance" runat="server" CssClass="control-label"></asp:Label>
            </center>
            <div id="showtext">
            </div>
            <div class="container">
                <div class="row">
                    <div class="col-sm-8">
                    </div>
                    <div class="col-sm-2">
                        <label class="control-label" style="visibility: hidden">
                            Debit</label></div>
                    <div class="col-sm-2">
                        <label class="control-label" style="visibility: hidden">
                            Credit</label></div>
                </div>
                <asp:Panel ID="PanelFee" runat="server" Style="overflow-x: auto;">
                </asp:Panel>
                <hr style="border-top: 1px double #8c8b8b" />
            </div>
            <div class="container">
                <h2>
                    <u>Bill Detail</u></h2>
                <table class="table">
                    <tbody>
                        <tr>
                            <td>
                                <h4>
                                    TOTAL PAYABLE (DR)
                                </h4>
                            </td>
                            <td>
                                <h4>
                                    <asp:Label ID="lblFinalTotal" runat="server" Font-Bold="true"></asp:Label>
                                </h4>
                            </td>
                        </tr>
                        <tr class="success">
                            <td>
                                <h4>
                                    RECEIVED AGAINST BILL CYCLE 2017</h4>
                            </td>
                            <td>
                                <h4>
                                    <asp:Label ID="lblReceive" runat="server" Font-Bold="true"></asp:Label>
                                </h4>
                            </td>
                        </tr>
                        <tr class="danger">
                            <td>
                                <h4>
                                    NET PAYABLE AMOUNT</h4>
                            </td>
                            <td>
                                <div class="left-inner-addon ">
                                    <i class="icon-user"></i>
                                    <input type="text" name="NetPayAmount" id="NetPayAmount" runat="server" class="form-control"
                                        placeholder="Net Payable Amount" />&nbsp;<span id="errmsg"></span>
                                </div>
                            </td>
                        </tr>
                        <tr class="Active">
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="btnPayment" runat="server" CssClass="btn btn-primary nextBtn btn-lg pull-right"
                                    OnClick="btnPayment_Click" Text="Pay Now" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="row">
                <div class="col-sm-2" style="visibility: hidden">
                    <asp:Label ID="lblDebit" runat="server" CssClass="form-control"></asp:Label>
                </div>
                <div class="col-sm-2" style="visibility: hidden">
                    <asp:Label ID="lblCredit" runat="server" CssClass="form-control"></asp:Label>
                </div>
                <div class="row">
                    <div class="col-sm-3" style="visibility: hidden">
                        <label class="control-label">
                            SUB-TOTAL</label>
                    </div>
                    <div class="col-sm-3" style="visibility: hidden">
                        <asp:Label ID="SubTotal" runat="server" BorderStyle="Solid"></asp:Label>
                    </div>
                    <div class="col-sm-3" style="visibility: hidden">
                        <h3>
                            Payment Mode</h3>
                        <asp:RadioButton ID="rdDebit" runat="server" AutoPostBack="true" GroupName="card"
                            OnCheckedChanged="rdDebit_CheckedChanged" Checked="true" Text="DebitCard" />
                        <asp:RadioButton ID="rdCredit" runat="server" AutoPostBack="true" GroupName="card"
                            OnCheckedChanged="rdCredit_CheckedChanged" Text="CreditCard" />
                        <asp:RadioButton ID="rdNetBanking" runat="server" AutoPostBack="true" GroupName="card"
                            OnCheckedChanged="rdNetBanking_CheckedChanged" Text="NetBanking" />
                    </div>
                </div>
                <%--<div class="loading" align="center">
                            Loading. Please wait.<br />
                            <br />
                            <img src="img/loading45.gif" alt="" />
                        </div>--%>
                <div class="row">
                    <div class="col-sm-3">
                        <label class="control-label" style="visibility: hidden">
                            Payment Staus</label>
                    </div>
                    <div class="col-sm-9">
                        <asp:Label ID="PaymentStatus" runat="server" BackColor="LightGreen" font-name="Lucida Console"
                            Font-Size="10pt" Width="45%"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- About Section -->
    <%--<section class="success" id="about"> <div class="container">
    <div class="row"> <div class="col-lg-12 text-center"> <h2>About</h2> <hr class="star-light">
    </div> </div> <div class="row"> <div class="col-lg-4 col-lg-offset-2"> <p>To attempt
    to measure the extent of an individuals success in making full use of his/her talent
    and opportunities. To develop fluency (oral and written) in the use of English (medium
    of instruction) as well as the mother tongue/national language. </p> </div> <div
    class="col-lg-4"> <p>To recognize and accept individual differences in ability and
    talent and to hold every child/individual in esteem in his/her own right. To attempt
    to identify and, as far as possible, meet the special needs of each individual/child
    in the college community, physical, intellectual, social, emotional, aesthetic and
    spiritual. </p> </div> </div> </div> </section> <!-- Contact Section --> <section
    id="contact"> <div class="container"> <div class="row"> <div class="col-lg-12 text-center">
    <h2>Contact Me</h2> <hr class="star-primary"> </div> </div> <div class="row"> <div
    class="col-lg-8 col-lg-offset-2"> Principal Sherwood College Nainital - 263002 Uttarakhand,
    INDIA </div> </div> </div>--%>
    </section>
    <div class="footer">
    </div>
    </form>
</body>
</html>
