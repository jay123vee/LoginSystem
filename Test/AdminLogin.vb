Public Class AdminLogin
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If txtAdminUsername.Text = "" Then
            MessageBox.Show("Please enter username")
            txtAdminUsername.Focus()
            Exit Sub
        ElseIf txtAdminPassword.Text = "" Then
            MessageBox.Show("Please enter password")
            txtAdminPassword.Focus()
            Exit Sub
        End If

        If txtAdminUsername.Text = "admin" And txtAdminPassword.Text = "admin" Then
            MessageBox.Show("welcome admin")
            Me.Hide()
            Admin.Show()
        Else
            MessageBox.Show("incorrect username or password")
        End If
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Close()
        Login.Show()
    End Sub
End Class