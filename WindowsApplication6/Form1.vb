﻿Imports System.Data.SqlClient
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

   
    Dim con As SqlConnection = New SqlConnection("Data Source=EUMA\SQLEXPRESS;Initial Catalog=db;Integrated Security=True")
    Dim conn As New MySql.Data.MySqlClient.MySqlConnection
    Dim myConnectionString As String
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

      
        myConnectionString = "server=127.0.0.1;" _
            & "uid=root;" _
            & "pwd=;" _
            & "database=db"

        conn.ConnectionString = myConnectionString
        conn.Open()

        Using con As New MySqlConnection(myConnectionString)
            Using cmd As New MySqlCommand("SELECT items.id,items.name FROM items ", conn)
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



                        DataGridView1.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                        DataGridView1.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter



                    End Using
                End Using
            End Using
        End Using
    End Sub

   


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

      
        Using con1 As New MySqlConnection(myConnectionString)
            Using cmd1 As New MySqlCommand("Select COUNT(*) FROM items where name ='" + TextBox1.Text + "'", conn)
                cmd1.CommandType = CommandType.Text

                If cmd1.ExecuteScalar > 0 Then
                    MsgBox("Item is already registered.", MsgBoxStyle.Exclamation, "Error")

                Else

                    If TextBox1.Text = "" Then
                        MsgBox("No no no", MsgBoxStyle.Exclamation, "Process Complete")
                    Else
                        Using con As New MySqlConnection(myConnectionString)
                            Using cmd As New MySqlCommand(" INSERT INTO `db`.`items` (`name`) VALUES ('" + TextBox1.Text + "');", conn)
                                cmd.CommandType = CommandType.Text

                                If cmd.ExecuteNonQuery > 0 Then
                                    MsgBox("Successfully added to database", MsgBoxStyle.Exclamation, "Process Complete")
                                    Using cmd2 As New MySqlCommand("SELECT items.id,items.name FROM items ", conn)
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

                                End If
                            End Using
                        End Using
                    End If
                End If




            End Using
        End Using




    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Dim senderGrid = DirectCast(sender, DataGridView)

        If TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn AndAlso
           e.RowIndex >= 0 Then

            If e.ColumnIndex = 0 Then
                If Panel3.Visible = False Then
                    Panel3.Visible = True
                    takara.Text = "Item name: " & DataGridView1.Rows(e.RowIndex).Cells(4).Value.ToString()

                    Using con As New MySqlConnection(myConnectionString)
                        Using cmd As New MySqlCommand("SELECT itemcontent.id,itemcontent.modelnumber,tag.description FROM items left outer join itemcontent on itemcontent.itemID = items.id left outer join tag on itemcontent.tagID = tag.id where items.id =" & DataGridView1.Rows(e.RowIndex).Cells(3).Value.ToString(), conn)
                            cmd.CommandType = CommandType.Text
                            If IsDBNull(cmd) Then
                                MessageBox.Show("No record")
                            Else
                                Using sda As New MySqlDataAdapter(cmd)
                                    Using dt As New DataTable()
                                        sda.Fill(dt)
                                        DataGridView2.DataSource = dt
                                        DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill
                                        DataGridView2.Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                                        DataGridView2.Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                                        DataGridView2.Columns(2).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                                        DataGridView2.Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                                        DataGridView2.Columns(4).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter


                                        DataGridView2.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                                        DataGridView2.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                                        DataGridView2.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


                                    End Using
                                End Using
                            End If
                        End Using
                    End Using
                Else
                    Panel3.Visible = False

                End If
            End If

            If e.ColumnIndex = 1 Then
                DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString()
                If Panel2.Visible = False Then
                    Panel2.Visible = True
                    TextBox4.Text = DataGridView1.Rows(e.RowIndex).Cells(4).Value.ToString()
       
                    TextBox5.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value.ToString()




                Else
                    Panel2.Visible = False

                End If
            End If
            If e.ColumnIndex = 2 Then


                Using con As New MySqlConnection(myConnectionString)
                    Using cmd As New MySqlCommand("DELETE FROM items WHERE id =" + DataGridView1.Rows(e.RowIndex).Cells(3).Value.ToString(), conn)
                        cmd.CommandType = CommandType.Text

                        If cmd.ExecuteNonQuery > 0 Then
                            MsgBox("Successfully Deleted", MsgBoxStyle.Exclamation, "Process Complete")

                            Using cmd1 As New MySqlCommand("SELECT items.id,items.name FROM items ", conn)
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
        If TextBox4.Text = "" Then
            MsgBox("No no no", MsgBoxStyle.Exclamation, "Process Complete")
        Else

            Using con As New MySqlConnection(myConnectionString)
                Using cmd As New MySqlCommand(" UPDATE `db`.`items` SET `name`='" + TextBox4.Text + "' WHERE (`id` = '" & TextBox5.Text & "');", conn)
                    cmd.CommandType = CommandType.Text

                    If cmd.ExecuteNonQuery > 0 Then
                        MsgBox("Successfully updated in the database", MsgBoxStyle.Exclamation, "Process Complete")
                        Using cmd2 As New MySqlCommand("SELECT items.id,items.name FROM items ", conn)
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

                    End If
                End Using
            End Using
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Panel3.Visible = False
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

    End Sub
End Class
