<%@ Page Title="" Language="C#" MasterPageFile="~/masterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="TelaBloqueada.aspx.cs" Inherits="Sistema.Web.UI.Sistema.TelaBloqueada" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="logo">
					<h1 class="semi-bold"><img src="img/logo-o.png" alt="" /> SmartAdmin</h1>
				</div>
				<div>
					<img src="img/avatars/sunny-big.png" alt="" width="120" height="120" />
					<div>
						<h1><i class="fa fa-user fa-3x text-muted air air-top-right hidden-mobile"></i>John Doe <small><i class="fa fa-lock text-muted"></i> &nbsp;Locked</small></h1>
						<p class="text-muted">
							<a href="mailto:simmons@smartadmin">john.doe@smartadmin.com</a>
						</p>

						<div class="input-group">
							<input class="form-control" type="password" placeholder="Password">
							<div class="input-group-btn">
								<button class="btn btn-primary" type="submit">
									<i class="fa fa-key"></i>
								</button>
							</div>
						</div>
						<p class="no-margin margin-top-5">
							Logged as someone else? <a href="login.html"> Click here</a>
						</p>
					</div>

				</div>
				<p class="font-xs margin-top-5">
					Copyright SmartAdmin 2014-2020.
				</p>


</asp:Content>
