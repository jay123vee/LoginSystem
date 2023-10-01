Imports System.IO

Public Class Login
    Private login_attempt As Integer = 0
    Private loginTimer As New Timer()

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles Me.Load
        AddHandler loginTimer.Tick, AddressOf EnableLoginForm
        loginTimer.Interval = 5000
        loginTimer.Enabled = False
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        ' Login Attempts
        If login_attempt >= 3 Then
            Me.Enabled = False
            loginTimer.Start()
            Return
        End If

        ' Get the entered username and password
        Dim enteredUsername As String = txtUsername.Text
        Dim enteredPassword As String = txtPassword.Text

        If txtUsername.Text = "" AndAlso txtPassword.Text = "" Then
            MsgBox("Enter username and password")
        ElseIf UserExists(enteredUsername) Then
            If CheckLogin(enteredUsername, enteredPassword) Then
                MsgBox("Login successful.")
                Welcome.Show()
                Me.Hide()
            Else
                login_attempt += 1
                MsgBox("Login failed. Please check your password.")
                txtPassword.Clear()
                txtPassword.Focus()
            End If
        Else
            MsgBox("User not found. Please check your username.")
            txtUsername.Clear()
            txtUsername.Focus()
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

    ' Function to check if the entered username exists in the stored data
    Private Function UserExists(username As String) As Boolean
        ' Read the registration data from your text file (change the file path)
        Dim filePath As String = Path.Combine(Application.StartupPath, "Users.txt")

        Try
            ' Read all lines from the text file
            Dim lines() As String = System.IO.File.ReadAllLines(filePath)

            ' Loop through each line and check for a matching username
            For Each line As String In lines
                Dim fields() As String = line.Split(","c)
                If fields.Length = 4 AndAlso fields(0) = username Then
                    ' Username exists in the data
                    Return True
                End If
            Next
        Catch ex As Exception
            ' Handle file reading errors (e.g., file not found)
        End Try

        ' If no matching username was found, return false
        Return False
    End Function

    ' Function to check login credentials
    Private Function CheckLogin(username As String, password As String) As Boolean
        ' Read the registration data from your text file (change the file path)
        Dim filePath As String = Path.Combine(Application.StartupPath, "Users.txt")

        Try
            ' Read all lines from the text file
            Dim lines() As String = System.IO.File.ReadAllLines(filePath)

            ' Loop through each line and check for a matching username, password, and status
            For Each line As String In lines
                Dim fields() As String = line.Split(","c)
                If fields.Length = 4 AndAlso fields(0) = username AndAlso fields(1) = password AndAlso fields(3) = "Approved" Then
                    ' Username, password, and status match, user is authenticated
                    Return True
                End If
            Next
        Catch ex As Exception
            ' Handle file reading errors (e.g., file not found)
        End Try

        ' If no matching user was found, or the password doesn't match, return false
        Return False
    End Function
End Class
