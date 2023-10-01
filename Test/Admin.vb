Imports System.IO

Public Class Admin
    ' Define a DataTable to store pending registrations
    Dim pendingRegistrationsTable As New DataTable()

    Private Sub AdminForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Configure the DataGridView to display registration data
        DataGridView1.DataSource = pendingRegistrationsTable
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        ' Add columns to the DataTable
        pendingRegistrationsTable.Columns.Add("Username")
        pendingRegistrationsTable.Columns.Add("Password")
        pendingRegistrationsTable.Columns.Add("FullName")
        pendingRegistrationsTable.Columns.Add("Status")

        ' Load pending registrations from the file
        LoadPendingRegistrations()
    End Sub

    ' Load pending registrations from the file
    Private Sub LoadPendingRegistrations()
        ' Read the "PendingRegistrations.txt" file (you can change the file path)
        Dim filePath As String = Path.Combine(Application.StartupPath, "Users.txt")

        If System.IO.File.Exists(filePath) Then
            Dim lines As String() = System.IO.File.ReadAllLines(filePath)

            For Each line As String In lines
                Dim parts As String() = line.Split(","c)
                If parts.Length = 4 Then
                    pendingRegistrationsTable.Rows.Add(parts)
                End If
            Next
        End If
    End Sub

    ' Approve a registration
    Private Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim selectedRowIndex As Integer = DataGridView1.SelectedRows(0).Index
            pendingRegistrationsTable.Rows(selectedRowIndex)("Status") = "Approved"
            SavePendingRegistrations()
        Else
            MessageBox.Show("Select a registration to approve.")
        End If
    End Sub

    ' Reject a registration
    Private Sub btnReject_Click(sender As Object, e As EventArgs) Handles btnReject.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim selectedRowIndex As Integer = DataGridView1.SelectedRows(0).Index
            pendingRegistrationsTable.Rows(selectedRowIndex)("Status") = "Rejected"
            SavePendingRegistrations()
        Else
            MessageBox.Show("Select a registration to reject.")
        End If
    End Sub

    ' Refresh the DataGridView
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        RefreshDataGridView()
    End Sub

    ' Log out the admin
    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        Me.Hide()
        Login.Show()
    End Sub

    ' Save the updated pending registrations to the file
    Private Sub SavePendingRegistrations()
        Dim filePath As String = Path.Combine(Application.StartupPath, "Users.txt")

        Try
            Using writer As New System.IO.StreamWriter(filePath, False)
                For Each row As DataRow In pendingRegistrationsTable.Rows
                    writer.WriteLine($"{row("Username")},{row("Password")},{row("FullName")},{row("Status")}")
                Next
            End Using
        Catch ex As Exception
            MessageBox.Show("Error saving pending registrations.")
        End Try
    End Sub

    Private Sub RefreshDataGridView()
        ' Clear the existing data in the DataGridView and DataTable
        DataGridView1.DataSource = Nothing
        pendingRegistrationsTable.Rows.Clear()

        ' Reload the pending registrations
        LoadPendingRegistrations()

        ' Re-bind the DataTable to the DataGridView
        DataGridView1.DataSource = pendingRegistrationsTable
    End Sub
End Class
