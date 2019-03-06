<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRUDAlumno.aspx.cs" Inherits="CRUD_Alumnos.WebForms.CRUDAlumno" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous" />
    <title>CRUD Alumnos con WEBFORMS</title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>ALUMNOS</h2>
        <asp:Button Text="Refresh" runat="server" OnClick="Page_Load" />
        <div id="listarAlumnos">

            <asp:GridView ID="gvAlumnos" runat="server" EmptyDataText="Vacío" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

            </asp:GridView>

        </div>

        <br />
        <br />
        <hr/>

        <div id="AgregarAlumno" class="form-group">

            <h3>AGREGAR ALUMNO</h3>

            <label for="tbNombre" class="control-label col-sm-2">Ingrese Nombre: </label>
            <div class="col-sm-10">
                <asp:TextBox ID="tbNombre" runat="server"></asp:TextBox>
            </div>

            <label for="tbApellido" class="control-label col-sm-2">Ingrese Apellido: </label>
            <div class="col-sm-10">
                <asp:TextBox ID="tbApellido" runat="server"></asp:TextBox>
            </div>

            <label for="tbEdad" class="control-label col-sm-2">Ingrese Edad: </label>
            <div class="col-sm-10">
                <asp:TextBox ID="tbEdad" runat="server" TextMode="Number"></asp:TextBox>
            </div>

            <label for="ddlSexo" class="control-label col-sm-2">Sexo: </label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlSexo" runat="server">
                    <asp:ListItem Text="Femenino" Value="F" />
                    <asp:ListItem Text="Masculino" Value="M" />
                </asp:DropDownList><br />
            </div>

            <label for="ddlCiudades" class="control-label col-sm-2">Ingrese Ciudad: </label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlCiudades" runat="server"></asp:DropDownList>
            </div>

            <div class="control-label col-sm-2">
                <asp:Button ID="btnAgregar" Text="Agregar" runat="server" OnClick="btnAgregar_Click" class="btn btn-primary" style="margin-top:15px"/>
            </div>
        </div>
        <br />

        <hr/>

        <div id="BuscarAlumno">
            <h3>Buscar alumno</h3>
            <label for="tbBuscaAlumno">Ingrese el ID del alumno</label>
            <asp:TextBox ID="tbBuscaAlumno" runat="server" TextMode="Number"/>
            <asp:Button id="btnBuscarAlumno" Text="Buscar" runat="server" CssClass="btn btn-primary" OnClick="btnBuscarAlumno_Click"/>
            <asp:Label ID="lblNotFound" Text="No se encontraron resultados" runat="server" visible="false"/>
            <div id="BuscarAlumnoResultado" runat="server" visible="false">
                <h5>Resultado: </h5>

                <label>ID: </label>
                <asp:Label ID="lblAlumnoResultado_ID" runat="server" Font-Bold="true"/><br />

                <label>Nombre: </label>
                <asp:Label ID="lblAlumnoResultado_Nombre" runat="server" Font-Bold="true"/><br />
                <asp:TextBox ID="tbEditarAlumno_Nombre" runat="server" Visible="false"/><br />

                <label>Apellido: </label>
                <asp:Label ID="lblAlumnoResultado_Apellido" runat="server" Font-Bold="true"/><br />
                <asp:TextBox ID="tbEditarAlumno_Apellido" runat="server" Visible="false"/><br />

                <label>Edad: </label>
                <asp:Label ID="lblAlumnoResultado_Edad" runat="server" Font-Bold="true"/><br />
                <asp:TextBox ID="tbEditarAlumno_Edad" runat="server" Visible="false" TextMode="Number"/><br />

                <label>Sexo: </label>
                <asp:Label ID="lblAlumnoResultado_Sexo" runat="server" Font-Bold="true"/><br />
                <asp:DropDownList ID="ddlEditarAlumno_Sexo" runat="server" Visible="false">
                    <asp:ListItem Text="Femenino" Value="F"/>
                    <asp:ListItem Text="Masculino" Value="M"/>
                </asp:DropDownList><br />

                <label>Ciudad: </label>
                <asp:Label ID="lblAlumnoResultado_Ciudad" runat="server" Font-Bold="true"/><br />
                <asp:DropDownList ID="ddlEditarAlumno_Ciudad" runat="server" Visible="false"></asp:DropDownList><br />

                <asp:Label Text="Borrado con éxito" runat="server" id="lblBorradoConExito" Visible="false"/><br />

                <asp:Button Text="Editar" runat="server" ID="btnEditarAlumno" CssClass="btn btn-primary" OnClick="btnEditarAlumno_Click"/>
                <asp:Button Text="Eliminar" runat="server" ID="btnEliminarAlumno" CssClass="btn btn-danger" OnClick="btnEliminarAlumno_Click" />
                <asp:Button Text="Guardar" runat="server" ID="btnGuardarEdicion" CssClass="btn btn-success" OnClick="btnGuardarEdicion_Click" Visible="false"/>

            </div>
        </div>

        <hr/>
    </form>
</body>

<script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js" integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1" crossorigin="anonymous"></script>
<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js" integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM" crossorigin="anonymous"></script>

<script type="text/javascript">
    $(document).ready(function () {
        $("#btnGuardarEdicion").click(function () {

            var campo_nombre = $("#tbEditarAlumno_Nombre").val().trim();
            var campo_apellido = $("#tbEditarAlumno_Apellido").val().trim();
            var campo_edad = $("#tbEditarAlumno_Edad").val().trim();

            //si esta vacio lanza error
            if (campo_nombre.length == 0 ||
                campo_apellido.length == 0 ||
                campo_edad.length == 0) {
                alert("EDITAR ALUMNO: No puede haber campos vacios");
            }
        });

        $("#btnAgregar").click(function () {

            var campo_nombre = $("#tbNombre").val().trim();
            var campo_apellido = $("#tbApellido").val().trim();
            var campo_edad = $("#tbEdad").val().trim();

            //si esta vacio lanza error
            if (campo_nombre.length == 0 ||
                campo_apellido.length == 0 ||
                campo_edad.length == 0) {
                alert("AGREGAR ALUMNO: No puede haber campos vacios");
            }
        });

        $("#btnBuscarAlumno").click(function () {

            var campo_idalumno = $("#tbBuscaAlumno").val().trim();

            //si esta vacio lanza error
            if (campo_idalumno.length == 0) {
                alert("BUSCAR ALUMNO: Ingrese el ID");
            }
        });
    });
</script>
</html>
