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

    // Filter active items first
    const activeItems = items?.filter(item => !item.isDeleted);

    // Check empty
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
    const confirmDelete = confirm("Are you sure you want to delete?");
    if (!confirmDelete) return;

    $.ajax({
        url: "/Items/Delete?id=" + id,
        method: "DELETE",
        success: function (res) {
            if (res.success) {
                loadItems();
            } else {
                alert(res.message || "Delete failed");
            }
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

    $("#itemId").val("");
    $("#name").val("");
    $("#price").val("");
}


function saveItem(e) {
    e.preventDefault();

    const id = $("#itemId").val();
    const name = $("#name").val();
    const price = parseFloat($("#price").val());

    if (!id) {
        $.ajax({
            url: "/Items/Create",
            method: "POST",
            contentType: "application/json",
            data: JSON.stringify({ name: name, price: price }),
            success: function (res) {
                if (res.success) {
                    $("#itemForm")[0].reset();
                    $("#formSection").hide();
                    loadItems();
                } else {
                    alert(res.message || "Create failed");
                }
            },
            error: function () {
                alert("Create failed");
            }
        });
    } else {
        $.ajax({
            url: "/Items/Edit",
            method: "PUT",
            contentType: "application/json",
            data: JSON.stringify({ id: parseInt(id), name: name, price: price }),
            success: function (res) {
                if (res.success) {
                    $("#itemForm")[0].reset();
                    $("#formSection").hide();
                    loadItems();
                } else {
                    alert(res.message || "Update failed");
                }
            },
            error: function () {
                alert("Update failed");
            }
        });
    }
}