Imports System.IO

Public Class ForgotPassword
    Private Sub btnResetPassword_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Dim enteredUsername As String = txtUsername.Text
        Dim newPassword As String = txtNewPassword.Text

        ' Check if the entered username exists in the stored data
        If UserExists(enteredUsername) Then
            ' Update the password in the stored data
            If UpdatePassword(enteredUsername, newPassword) Then
                MsgBox("Password reset successful.")
                txtUsername.Clear()
                txtNewPassword.Clear()
                txtUsername.Focus()
            Else
                MsgBox("Password reset failed. Please try again.")
            End If
        Else
            MsgBox("User not found. Please check your username.")
        End If
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

    ' Function to update the password in the stored data
    Private Function UpdatePassword(username As String, newPassword As String) As Boolean
        ' Read the registration data from your text file (change the file path)
        Dim filePath As String = Path.Combine(Application.StartupPath, "Users.txt")

        Try
            ' Read all lines from the text file
            Dim lines() As String = System.IO.File.ReadAllLines(filePath)

            ' Create a StringBuilder to build the updated data
            Dim updatedData As New System.Text.StringBuilder()

            ' Loop through each line and update the password for the matching username
            For Each line As String In lines
                Dim fields() As String = line.Split(","c)
                If fields.Length = 4 AndAlso fields(0) = username Then
                    ' Update the password
                    fields(1) = newPassword
                End If
                ' Reconstruct the line with updated or unchanged data
                updatedData.AppendLine(String.Join(",", fields))
            Next

            ' Write the updated data back to the file
            System.IO.File.WriteAllText(filePath, updatedData.ToString())

            ' Password update successful
            Return True
        Catch ex As Exception
            ' Handle file reading/writing errors
        End Try

        ' Password update failed
        Return False
    End Function

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Hide()
        Login.Show()
    End Sub
End Class
