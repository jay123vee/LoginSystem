﻿Imports System.IO

Public Class Registration
    Public dbService As DatabaseService
    Public encryptService As EncryptionService




    Private Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        ' Get the entered registration details
        Dim username As String = txtRegUsername.Text
        Dim password As String = txtRegPassword.Text
        Dim fullName As String = txtFullname.Text
        Dim age As Integer = nudAge.Value
        Dim birthday As Date = dtpBirthDate.Text
        Dim role = "user"
        Dim user As New User()

        dbService = New DatabaseService()
        encryptService = New EncryptionService()





        Dim encryptedPassword = encryptService.EncryptText(password, encryptService.GenerateRandomKey(), encryptService.GenerateRandomIV())
        user.SetPassword(encryptedPassword)

        user.SetRole(role)
        user.SetUserName(username)
        'user.SetPassword(password)
        user.SetPending(True)
        user.SetFullName(fullName)
        user.SetBirthDate(birthday)
        user.SetAge(age)

        Dim registration = dbService.saveUserToDataBase(user)

        ' Save the registration details with a "Pending" status
        If registration Then
            MsgBox("Registration successful. Your account is pending admin approval.")
            txtFullname.Clear()
            txtRegUsername.Clear()
            txtRegPassword.Clear()
        Else
            MsgBox("Registration failed. Please try again.")
            txtFullname.Focus()
        End If
    End Sub

    Private Sub btnBack_Click_1(sender As Object, e As EventArgs) Handles btnBack.Click
        Login.Show()
        Me.Hide()
    End Sub


End Class
