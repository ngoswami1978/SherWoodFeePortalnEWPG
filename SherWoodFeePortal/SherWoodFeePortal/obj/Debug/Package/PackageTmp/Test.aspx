<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="SherWoodFeePortal.Test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <!-- jQuery -->
    <script src="vendor/jquery/jquery.min.js"></script>
    <!-- Bootstrap Core JavaScript -->
   <script src="vendor/bootstrap/js/bootstrap.min.js"></script>
    <!-- Plugin JavaScript -->
    <%--<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-easing/1.3/jquery.easing.min.js"></script>--%>
    <!-- Contact Form JavaScript -->
    <%--<script src="js/jqBootstrapValidation.js"></script>
    <script src="js/contact_me.js"></script>--%>
    <!-- Theme JavaScript -->
  <%--  <script src="js/freelancer.min.js"></script>--%>
     <link href="Styles/Stepwizard.css" rel="stylesheet" type="text/css" />
     <link href="vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">
 <script src="Scripts/StepWizard.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
   <div class="container">

   <asp:Panel ID="Panel" runat="server" style="overflow-x:auto;">

  <table class= "table table-user-information">
                <tbody>
                  <tr>
                    <td>Dokter Umum:</td>
                    <td>terap_addressdoc ?></td>
                  </tr>
                  <tr>
                    <td>Specialist Chiropactic</td>
                    <td><?php echo $row->terap_specialist ?></td>
                  </tr>

                   <tr>
                    <td>Email</td>
                    <td><?php echo $row->terap_terapist ?></td>
                  </tr>
               </tbody>
              </table>  
    </div> 
  
</div>
    </form>
</body>
</html>
