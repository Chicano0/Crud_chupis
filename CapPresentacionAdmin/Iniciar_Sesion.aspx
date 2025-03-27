<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Iniciar_Sesion.aspx.cs" Inherits="CapPresentacionAdmin.Iniciar_Sesion" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Iniciar Sesión</title>
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/5.3.0/css/bootstrap.min.css" />
    <!-- Custom CSS -->
    <style>
        * {
            margin: 0;
            padding: 0;
            font-family: Arial, Helvetica, sans-serif;
        }

        body {
            background-color: #121212;
            height: 100vh;
            width: 100%;
            display: flex;
            justify-content: center;
            align-items: center;
            background: linear-gradient(rgba(0,0,0,0.5), rgba(0,0,0,0.5)), url('https://blog.orange.es/wp-content/uploads/sites/4/2024/03/fondos-de-pantalla-3d-paisaje.jpg');
            background-position: center;
            background-size: cover;
        }

        .container-page { 
            height: 50vh;
            width: 20vw;
            border-radius: 20px;
            display: flex;
            justify-content: center;
            align-items: center;
            padding: 1rem;
            transition: all 0.8s;
            background-color: rgba(255, 255, 255, 0.1);
            border: 1px solid white;
            backdrop-filter: blur(10px);
        }

        .login-container {
            width: 100%;
            height: fit-content;
            display: flex;
            justify-content: center;
            align-items: center;
            flex-direction: column;
            color: white;
        }

        form {
            width: 80%;
            margin: auto;
        }

        .input-line-container {
            display: flex;
            justify-content: center;
            align-items: flex-start;
            flex-direction: column;
            width: 100%;
            margin: 1rem 0;
        }

        .name-input {
            font-size: 12px;
        }

        .input-line {
            width: 100%;
            height: 30px;
            background-color: transparent;
            border-top: none;
            border-left: none;
            border-right: none;
            border-bottom: 2px solid #CBCBCB;
            outline: none;
            margin-bottom: 1rem;
            color: white;
        }

        .button-login {
            width: 100%;
            height: 45px;
            border-radius: 55px;
            background: rgba(255, 255, 255, 0.137);
            color: white;
            font-size: 14px;
            font-weight: 600;
            text-transform: uppercase;
            border: none;
            margin-bottom: 1rem;
            cursor: pointer;
            transition: all 0.4s ease-in-out;
        }

        .button-second {
            width: 100%;
            height: 45px;
            border-radius: 55px;
            background: transparent;
            color: white;
            font-size: 14px;
            font-weight: 600;
            text-transform: uppercase;
            border: 2px solid white;
            margin-bottom: 1rem;
            cursor: pointer;
            transition: all 0.4s ease-in-out;
            opacity: 0.6;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .button-login:hover {
            box-shadow: 0px 0px 8px #ffffffa4;
        }

        .button-second:hover {
            opacity: 1;
        }

        #loginMessage {
            display: none;
            width: 100%;
            margin: 1rem 0;
        }
    </style>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <!-- Bootstrap JS -->
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/5.3.0/js/bootstrap.bundle.min.js"></script>
    <!-- SweetAlert2 JS -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <!-- Custom JS -->
    <script>
        $(document).ready(function () {
            history.replaceState(null, null, location.href);
            window.onpopstate = function (event) {
                history.replaceState(null, null, location.href);
                window.history.forward();
            };

            var loginMessage = document.getElementById('loginMessage');
            if (loginMessage && loginMessage.innerText.trim() !== '') {
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: loginMessage.innerText.trim()
                });
            }
        });

        function preventBack() {
            window.history.pushState(null, null, window.location.href);
            window.onpopstate = function () {
                window.history.go(1);
            };
        }
        preventBack(); 

        window.addEventListener('pageshow', function (event) {
            if (event.persisted || (window.performance && window.performance.navigation.type === 2)) {
                preventBack();
            }
        });
    </script>
</head>
<body>
    <div class="container-page" id="Container">
        <div class="login-container" id="LoginContainer">
            <h1 class="title">Iniciar Sesión</h1>
            <form id="form1" runat="server" method="post">
                <div class="input-line-container">
                    <span class="name-input">Email</span>
                    <input type="text" name="email" class="input-line" id="email" />
                </div>
                <div class="input-line-container">
                    <span class="name-input">Password</span>
                    <input type="password" name="password" class="input-line" id="password" />
                </div>
                <input type="submit" value="Login" class="button-login" />
                <asp:Label ID="loginMessage" runat="server" CssClass="d-none"></asp:Label>
            </form>
        </div>
    </div>
</body>
</html>
