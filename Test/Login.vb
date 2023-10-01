Imports System.IO

Public Class Login
    Public dbService As DatabaseService

    Private login_attempt As Integer = 0
    Private loginTimer As New Timer()

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles Me.Load
        AddHandler loginTimer.Tick, AddressOf EnableLoginForm
        loginTimer.Interval = 5000
        loginTimer.Enabled = False
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        dbService = New DatabaseService()

        Dim user = dbService.getUserByUserName(txtUsername.Text)



        If user Is Nothing Then
            MsgBox("User not found.")
            login_attempt += 1
        Else
            If txtPassword.Text <> user.GetPassword Then
                MsgBox("Invalid Credentials.")
                login_attempt += 1
            Else
                If user IsNot Nothing AndAlso user.GetRole() = "user" AndAlso Not user.GetPending() Then
                    'redirect to user form
                End If

                If user IsNot Nothing AndAlso user.GetRole() = "admin" AndAlso Not user.GetPending() Then
                    'redirect to admin form
                End If
            End If
        End If

        ' Login Attempts
        If login_attempt >= 3 Then
            Me.Enabled = False
            loginTimer.Start()
            Return
        End If
    End Sub

    ' Enable login form
    Private Sub EnableLoginForm(sender As Object, e As EventArgs)
        Me.Enabled = True
        login_attempt = 0 ' Reset login attempts
        loginTimer.Stop()
    End Sub

    Private Sub btnAdmin_Click(sender As Object, e As EventArgs) Handles btnAdmin.Click
        AdminLogin.Show()
        Me.Hide()
    End Sub

    Private Sub lnkForgotPassword_Click(sender As Object, e As EventArgs) Handles lnkForgotPassword.Click
        ForgotPassword.Show()
        Me.Hide()
    End Sub

    Private Sub lnkRegister_Click(sender As Object, e As EventArgs) Handles lnkRegister.Click
        Registration.Show()
        Me.Hide()
    End Sub
End Class
