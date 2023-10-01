Imports System.IO

Public Class Registration
    Public dbService As DatabaseService

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

        user.SetRole(role)
        user.SetUserName(username)
        user.SetPassword(password)
        user.SetFullName(fullName)
        user.SetPending(True)
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

    Private Sub UsertableBindingNavigatorSaveItem_Click(sender As Object, e As EventArgs)
        Me.Validate()
        Me.UsertableBindingSource.EndEdit()
        Me.TableAdapterManager.UpdateAll(Me.Database1DataSet)

    End Sub
End Class
