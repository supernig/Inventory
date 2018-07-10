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

   
    Dim con As SqlConnection = New SqlConnection("Data Source=EUMA\SQLEXPRESS;Initial Catalog=db;Integrated Security=True")
    Dim conn As New MySql.Data.MySqlClient.MySqlConnection
    Dim myConnectionString As String
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        myConnectionString = "server=127.0.0.1;" _
            & "uid=root;" _
            & "pwd=root;" _
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
                        MsgBox("Inputs cannot be blank.", MsgBoxStyle.Exclamation, "Process Complete")
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
    Dim RecordCount As Int32 = 0
    Dim b As String
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Dim senderGrid = DirectCast(sender, DataGridView)

        If TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn AndAlso
           e.RowIndex >= 0 Then

            If e.ColumnIndex = 0 Then
                If Panel3.Visible = False Then
                    Panel3.Visible = True
                    takara.Text = "Item name: " & DataGridView1.Rows(e.RowIndex).Cells(4).Value.ToString()
                    tb3.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value.ToString()
                    b = DataGridView1.Rows(e.RowIndex).Cells(3).Value.ToString()
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

                                        Using con1 As New MySqlConnection(myConnectionString)
                                            Using cmd1 As New MySqlCommand("SELECT COUNT(itemcontent.id) from items left join itemcontent on itemcontent.itemID = items.id where  itemcontent.tagID = 1 AND items.id = " & DataGridView1.Rows(e.RowIndex).Cells(3).Value.ToString(), conn)
                                                cmd1.CommandType = CommandType.Text
                                                If IsDBNull(cmd1) Then
                                                    MessageBox.Show("No record")
                                                Else
                                                    Using sda1 As New MySqlDataAdapter(cmd1)
                                                        RecordCount = Convert.ToInt32(cmd1.ExecuteScalar())
                                                        Label10.Text = "Available Stocks: " & RecordCount.ToString



                                                    End Using
                                                End If
                                            End Using
                                        End Using

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
                            MsgBox("Something went wrong.", MsgBoxStyle.Exclamation, "Error")



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
            MsgBox("Inputs cannot be blank", MsgBoxStyle.Exclamation, "Process Complete")
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
        If Panel4.Visible = False Then
            Panel4.Visible = True

        Else
            Panel4.Visible = False

        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles tb2.TextChanged

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Panel4.Visible = False
    End Sub
    Dim a As String
    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick
        Dim senderGrid = DirectCast(sender, DataGridView)

        If TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn AndAlso
           e.RowIndex >= 0 Then

            If e.ColumnIndex = 0 Then

                If Panel5.Visible = False Then
                    Panel5.Visible = True
                    a = DataGridView2.Rows(e.RowIndex).Cells(2).Value.ToString()
                    'MsgBox(DataGridView2.Rows(e.RowIndex).Cells(2).Value.ToString(), MsgBoxStyle.Exclamation, "Error")
                    tbe1.Text = DataGridView2.Rows(e.RowIndex).Cells(3).Value.ToString()
                    cbe1.SelectedIndex = cbe1.Items.IndexOf(DataGridView2.Rows(e.RowIndex).Cells(4).Value.ToString())


                Else
                    Panel5.Visible = False

                End If

            End If

            If e.ColumnIndex = 1 Then




                Using con As New MySqlConnection(myConnectionString)
                        Using cmd As New MySqlCommand("DELETE FROM itemcontent WHERE id =" + DataGridView2.Rows(e.RowIndex).Cells(2).Value.ToString(), conn)
                            cmd.CommandType = CommandType.Text

                            If cmd.ExecuteNonQuery > 0 Then
                                MsgBox("Successfully Deleted", MsgBoxStyle.Exclamation, "Process Complete")

                                Using cmd1 As New MySqlCommand("SELECT itemcontent.id,itemcontent.modelnumber,tag.description FROM items left outer join itemcontent on itemcontent.itemID = items.id left outer join tag on itemcontent.tagID = tag.id where items.id =" & tb3.Text, conn)
                                    cmd1.CommandType = CommandType.Text
                                    Using sda As New MySqlDataAdapter(cmd1)
                                        Using dt As New DataTable()


                                            sda.Fill(dt)
                                            Dim bSource As New BindingSource()
                                            bSource.DataSource = dt
                                        DataGridView2.DataSource = bSource
                                        bSource.ResetBindings(False)
                                        DataGridView2.Refresh()

                                        Using con1 As New MySqlConnection(myConnectionString)
                                            Using cmd2 As New MySqlCommand("SELECT COUNT(itemcontent.id) from items left join itemcontent on itemcontent.itemID = items.id where  itemcontent.tagID = 1 AND items.id = " & DataGridView1.Rows(e.RowIndex).Cells(3).Value.ToString(), conn)
                                                cmd2.CommandType = CommandType.Text
                                                If IsDBNull(cmd2) Then
                                                    MessageBox.Show("No record")
                                                Else
                                                    Using sda1 As New MySqlDataAdapter(cmd2)
                                                        RecordCount = Convert.ToInt32(cmd2.ExecuteScalar())
                                                        Label10.Text = "Available Stocks: " & RecordCount.ToString



                                                    End Using
                                                End If
                                            End Using
                                        End Using
                                    End Using
                                    End Using
                                End Using

                            Else
                            MsgBox("Something went wrong", MsgBoxStyle.Exclamation, "Error")



                        End If
                        End Using
                    End Using



                End If
        End If

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click

        Using con1 As New MySqlConnection(myConnectionString)
            Using cmd1 As New MySqlCommand("Select COUNT(*) FROM itemcontent where modelNumber ='" + tb2.Text + "' AND itemid=" & tb3.Text, conn)
                cmd1.CommandType = CommandType.Text

                If cmd1.ExecuteScalar > 0 Then
                    MsgBox("Item is already registered.", MsgBoxStyle.Exclamation, "Error")

                Else

                    If tb2.Text = "" Then
                        MsgBox("Inputs cannot be blank", MsgBoxStyle.Exclamation, "Process Complete")
                    Else
                        Using con As New MySqlConnection(myConnectionString)
                            Using cmd As New MySqlCommand(" INSERT INTO `db`.`itemcontent` (`itemid`,`tagID`,`modelNumber`,`StockID`) VALUES (" & tb3.Text & "," & cb2.SelectedIndex + 1 & ",'" & tb2.Text & "'," & tb3.Text & ");", conn)
                                cmd.CommandType = CommandType.Text

                                If cmd.ExecuteNonQuery > 0 Then
                                    MsgBox("Successfully added to database", MsgBoxStyle.Exclamation, "Process Complete")
                                    Panel4.Visible = False
                                    tb2.Text = ""
                                    Using cmd2 As New MySqlCommand("SELECT itemcontent.id,itemcontent.modelnumber,tag.description FROM items left outer join itemcontent on itemcontent.itemID = items.id left outer join tag on itemcontent.tagID = tag.id where items.id =" & tb3.Text, conn)
                                        cmd2.CommandType = CommandType.Text
                                        Using sda As New MySqlDataAdapter(cmd2)
                                            Using dt As New DataTable()
                                                sda.Fill(dt)
                                                DataGridView2.DataSource = dt

                                                DataGridView2.Update()

                                                Using con2 As New MySqlConnection(myConnectionString)
                                                    Using cmd3 As New MySqlCommand("SELECT COUNT(itemcontent.id) from items left join itemcontent on itemcontent.itemID = items.id where  itemcontent.tagID = 1 AND items.id = " & b, conn)
                                                        cmd3.CommandType = CommandType.Text
                                                        If IsDBNull(cmd3) Then
                                                            MessageBox.Show("No record")
                                                        Else
                                                            Using sda1 As New MySqlDataAdapter(cmd3)
                                                                RecordCount = Convert.ToInt32(cmd3.ExecuteScalar())
                                                                Label10.Text = "Available Stocks: " & RecordCount.ToString



                                                            End Using
                                                        End If
                                                    End Using
                                                End Using
                                            End Using
                                        End Using
                                    End Using


                                End If
                            End Using
                        End Using
                    End If
                End If




            End Using
        End Using

    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        If tbe1.Text = "" Then
            MsgBox("Inputs cannot be blank", MsgBoxStyle.Exclamation, "Process Complete")

        Else

            Using con As New MySqlConnection(myConnectionString)
                Using cmd As New MySqlCommand(" UPDATE `db`.`itemcontent` SET `modelnumber`='" + tbe1.Text + "', tagID='" & cbe1.SelectedIndex + 1 & "' WHERE (`id` = '" & a & "' );", conn)
                    cmd.CommandType = CommandType.Text

                    If cmd.ExecuteNonQuery > 0 Then
                        MsgBox("Successfully updated in the database", MsgBoxStyle.Exclamation, "Process Complete")
                        Using cmd2 As New MySqlCommand("SELECT itemcontent.id,itemcontent.modelnumber,tag.description FROM items left outer join itemcontent on itemcontent.itemID = items.id left outer join tag on itemcontent.tagID = tag.id where items.id =" & tb3.Text, conn)
                            cmd2.CommandType = CommandType.Text
                            Using sda As New MySqlDataAdapter(cmd2)
                                Using dt As New DataTable()

                                    sda.Fill(dt)
                                    Dim bSource As New BindingSource()
                                    bSource.DataSource = dt
                                    DataGridView2.DataSource = bSource
                                    bSource.ResetBindings(False)
                                    DataGridView2.Refresh()


                                    Using con1 As New MySqlConnection(myConnectionString)
                                        Using cmd3 As New MySqlCommand("SELECT COUNT(itemcontent.id) from items left join itemcontent on itemcontent.itemID = items.id where  itemcontent.tagID = 1 AND items.id = " & b, conn)
                                            cmd3.CommandType = CommandType.Text
                                            If IsDBNull(cmd3) Then
                                                MessageBox.Show("No record")
                                            Else
                                                Using sda1 As New MySqlDataAdapter(cmd3)
                                                    RecordCount = Convert.ToInt32(cmd3.ExecuteScalar())
                                                    Label10.Text = "Available Stocks: " & RecordCount.ToString



                                                End Using
                                            End If
                                        End Using
                                    End Using
                                End Using
                            End Using
                        End Using
                        Panel5.Visible = False


                    End If
                End Using
            End Using
        End If
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Panel5.Visible = False

    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint

    End Sub

    Private Sub Label10_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub cb2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb2.SelectedIndexChanged

    End Sub

    Private Sub takara_Click(sender As Object, e As EventArgs) Handles takara.Click

    End Sub
End Class
