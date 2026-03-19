$(document).ready(function () {
    loadItems();
    $("#loadItemsBtn").click(loadItems);
    $("#createNewBtn").click(showCreateForm);
    $("#itemForm").submit(saveItem);
});

function loadItems() {
    $.ajax({
        url: "/Items/GetAllItems",
        method: "GET",
        success: function (data) {
            renderTable(data);
        },
        error: function () {
            alert("Failed to load items");
        }
    });
}

function renderTable(items) {
    const tbody = $("#itemTableBody");
    tbody.empty();

    const activeItems = items?.filter(item => !item.isDeleted);

    if (!activeItems || activeItems.length === 0) {
        tbody.append(`
            <tr>
                <td colspan="4" style="text-align:center;">No items found!!</td>
            </tr>
        `);
        return;
    }

    activeItems.forEach(item => {
        tbody.append(`
            <tr>
                <td>${item.id}</td>
                <td>${item.name}</td>
                <td>${item.price}</td>
                <td>
                    <button onclick="editItem(${item.id})">Edit</button>
                    <button onclick="deleteItem(${item.id})">Delete</button>
                </td>
            </tr>
        `);
    });
}

function deleteItem(id) {
    if (!confirm("Are you sure you want to delete?")) return;

    $.ajax({
        url: "/Items/Delete?id=" + id,
        method: "DELETE",
        success: function (res) {
            if (res.success) loadItems();
            else alert(res.message || "Delete failed");
        },
        error: function () {
            alert("Delete failed");
        }
    });
}

function editItem(id) {
    $.ajax({
        url: "/Items/GetById?id=" + id,
        method: "GET",
        success: function (item) {
            $("#formSection").show();
            $("#formTitle").text("Update Item");

            $("#itemId").val(item.id);
            $("#name").val(item.name);
            $("#price").val(item.price);
        },
        error: function () {
            alert("Failed to load item");
        }
    });
}

function showCreateForm() {
    $("#formSection").show();
    $("#formTitle").text("Create Item");

    $("#itemForm")[0].reset();
    $("#itemId").val("");
}

function saveItem(e) {
    e.preventDefault();

    const id = $("#itemId").val();

    const payload = {
        id: id ? parseInt(id) : null,
        name: $("#name").val(),
        price: parseFloat($("#price").val())
    };

    console.log(payload);

    $.ajax({
        url: "/Items/Save",   
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify(payload),
        success: function (res) {
            if (res.success) {
                $("#itemForm")[0].reset();
                $("#formSection").hide();
                loadItems();
            } else {
                alert(res.message || "Save failed");
            }
        },
        error: function () {
            alert("Save failed");
        }
    });
}