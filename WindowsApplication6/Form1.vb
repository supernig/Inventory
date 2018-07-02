Imports System.Data.SqlClient
Imports System.Data
Imports MySql.Data.MySqlClient
Public Class Form1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Panel1.Visible = False Then
            Panel1.Visible = True

        Else
            Panel1.Visible = False

        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Panel1.Visible = False
    End Sub

    Private Sub ItemsBindingNavigatorSaveItem_Click(sender As Object, e As EventArgs)
        Me.Validate()
        Me.ItemsBindingSource.EndEdit()
        Me.TableAdapterManager.UpdateAll(Me.DbDataSet)

    End Sub
    Dim con As SqlConnection = New SqlConnection("Data Source=EUMA\SQLEXPRESS;Initial Catalog=db;Integrated Security=True")
    Dim conn As New MySql.Data.MySqlClient.MySqlConnection
    Dim myConnectionString As String
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'DbDataSet.items' table. You can move, or remove it, as needed.
        Me.ItemsTableAdapter.FillBy1(Me.DbDataSet.items)
        'TODO: This line of code loads data into the 'DbDataSet.items' table. You can move, or remove it, as needed.
        Me.ItemsTableAdapter.FillBy1(Me.DbDataSet.items)

        ComboBox1.SelectedIndex = 0

        myConnectionString = "server=127.0.0.1;" _
            & "uid=root;" _
            & "pwd=root;" _
            & "database=db"

        conn.ConnectionString = myConnectionString
        conn.Open()

        Using con As New MySqlConnection(myConnectionString)
            Using cmd As New MySqlCommand("SELECT items.id,items.name,items.stock,tag.description FROM items left outer join tag on tag.id = items.tagID", conn)
                cmd.CommandType = CommandType.Text
                Using sda As New MySqlDataAdapter(cmd)
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        DataGridView1.DataSource = dt
                        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill
                        DataGridView1.Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                        DataGridView1.Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                        DataGridView1.Columns(2).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                        DataGridView1.Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                        DataGridView1.Columns(4).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                        DataGridView1.Columns(5).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

                        DataGridView1.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                        DataGridView1.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                        DataGridView1.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                        DataGridView1.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                    End Using
                End Using
            End Using
        End Using
    End Sub

    Private Sub ItemsBindingNavigatorSaveItem_Click_1(sender As Object, e As EventArgs)
        Me.Validate()
        Me.ItemsBindingSource.EndEdit()
        Me.TableAdapterManager.UpdateAll(Me.DbDataSet)

    End Sub



    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim a = ComboBox1.SelectedIndex + 1


        Using con1 As New MySqlConnection(myConnectionString)
            Using cmd1 As New MySqlCommand("Select COUNT(*) FROM items where name ='" + TextBox1.Text + "'", conn)
                cmd1.CommandType = CommandType.Text

                If cmd1.ExecuteScalar > 0 Then
                    MsgBox("Item is already registered.", MsgBoxStyle.Exclamation, "Error")

                Else
                    Using con As New MySqlConnection(myConnectionString)
                        Using cmd As New MySqlCommand(" INSERT INTO `db`.`items` (`name`, `tagID`, `stock`) VALUES ('" + TextBox1.Text + "','" & a & "','" & TextBox2.Text & "');", conn)
                            cmd.CommandType = CommandType.Text

                            If cmd.ExecuteNonQuery > 0 Then
                                MsgBox("Successfully added to database", MsgBoxStyle.Exclamation, "Process Complete")
                                Using cmd2 As New MySqlCommand("SELECT items.id,items.name,items.stock,tag.description FROM items left outer join tag on tag.id = items.tagID", conn)
                                    cmd2.CommandType = CommandType.Text
                                    Using sda As New MySqlDataAdapter(cmd2)
                                        Using dt As New DataTable()
                                            sda.Fill(dt)
                                            DataGridView1.DataSource = dt

                                            DataGridView1.Update()

                                        End Using
                                    End Using
                                End Using
                                Panel1.Visible = False
                                TextBox1.Text = ""
                                TextBox2.Text = ""
                            End If
                        End Using
                    End Using

                End If




            End Using
        End Using




    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Dim senderGrid = DirectCast(sender, DataGridView)

        If TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn AndAlso
           e.RowIndex >= 0 Then

            If e.ColumnIndex = 0 Then
                DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString()
                If Panel1.Visible = False Then
                    Panel2.Visible = True
                    TextBox4.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value.ToString()
                    TextBox3.Text = DataGridView1.Rows(e.RowIndex).Cells(4).Value.ToString()
                    ComboBox2.SelectedItem = DataGridView1.Rows(e.RowIndex).Cells(5).Value.ToString()
                    TextBox5.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value.ToString()

                    Dim a = ComboBox1.SelectedIndex + 1












                Else
                    Panel2.Visible = False

                End If
            End If
            If e.ColumnIndex = 1 Then


                Using con As New MySqlConnection(myConnectionString)
                    Using cmd As New MySqlCommand("DELETE FROM items WHERE id =" + DataGridView1.Rows(e.RowIndex).Cells(2).Value.ToString(), conn)
                        cmd.CommandType = CommandType.Text

                        If cmd.ExecuteNonQuery > 0 Then
                            MsgBox("Successfully Deleted", MsgBoxStyle.Exclamation, "Process Complete")

                            Using cmd1 As New MySqlCommand("SELECT items.id,items.name,items.stock,tag.description FROM items left outer join tag on tag.id = items.tagID", conn)
                                cmd1.CommandType = CommandType.Text
                                Using sda As New MySqlDataAdapter(cmd1)
                                    Using dt As New DataTable()


                                        sda.Fill(dt)
                                        Dim bSource As New BindingSource()
                                        bSource.DataSource = dt
                                        DataGridView1.DataSource = bSource
                                        bSource.ResetBindings(False)
                                        DataGridView1.Refresh()
                                    End Using
                                End Using
                            End Using

                        Else
                            MsgBox("UY, WA MAN", MsgBoxStyle.Exclamation, "Error")



                        End If
                    End Using
                End Using

            End If




        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Panel2.Visible = False
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        Using con As New MySqlConnection(myConnectionString)
            Using cmd As New MySqlCommand(" UPDATE `db`.`items` SET `name`='" + TextBox4.Text + "', `tagID` = '" & ComboBox2.SelectedIndex + 1 & "', `stock` ='" & TextBox3.Text & "' WHERE (`id` = '" & TextBox5.Text & "');", conn)
                cmd.CommandType = CommandType.Text

                If cmd.ExecuteNonQuery > 0 Then
                    MsgBox("Successfully updated in the database", MsgBoxStyle.Exclamation, "Process Complete")
                    Using cmd2 As New MySqlCommand("SELECT items.id,items.name,items.stock,tag.description FROM items left outer join tag on tag.id = items.tagID", conn)
                        cmd2.CommandType = CommandType.Text
                        Using sda As New MySqlDataAdapter(cmd2)
                            Using dt As New DataTable()

                                sda.Fill(dt)
                                Dim bSource As New BindingSource()
                                bSource.DataSource = dt
                                DataGridView1.DataSource = bSource
                                bSource.ResetBindings(False)
                                DataGridView1.Refresh()

                            End Using
                        End Using
                    End Using
                    Panel2.Visible = False
                    TextBox1.Text = ""
                    TextBox2.Text = ""
                End If
            End Using
        End Using
    End Sub
End Class
