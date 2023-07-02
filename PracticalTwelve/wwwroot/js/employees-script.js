$(document).ready(() => {

    //refresh table data from DB
    const reloadTable = async () => {
        $("#EmployeeData").load("TestOne/LoadUsers");
    }

    //generate toast message
    const makeToast = (message) => {
        var url = 'Toast/Index?message=' + encodeURIComponent(message);
        $.get(url, (res) => {
            $("#ToastDisplay").html(res);
            $('.toast').toast('show');
        })
    }

    //onPageLoad events
    reloadTable();

    //all buttons
    let btns = $("#EmployeeTableActions .btn");

    //button event handlers
    Array.from(btns).forEach((btn) => {
        $(btn).on("click", (e) => {
            let eventId = e.target.id;
            let url = "TestOne/Index";
            let msg = "Operation performed successfully";
            switch (eventId) {
                case "InsertSingleRecord":
                    url = "TestOne/InsertSingleRecord";
                    msg = "record inserted.";
                    break;
                case "InsertMultipleRecord":
                    url = "TestOne/InsertMultipleRecord";
                    msg = "records inserted.";
                    break;
                case "UpdateFirstNameOfFirstRecord":
                    url = "TestOne/UpdateFirstNameOfFirstRecord";
                    msg = "record updated.";
                    break;
                case "UpdateMiddleNameOfAllRecords":
                    url = "TestOne/UpdateMiddleNameOfAllRecords";
                    msg = "records updated.";
                    break;
                case "DeleteHavingLessValueThanId":
                    if (!confirm('Are you sure you want delete records?')) reutrn;
                    url = "TestOne/DeleteHavingLessValueThanId";
                    msg = "record deleted.";
                    break;
                case "DeleteAllData":
                    if (!confirm('Are you sure you want to delete all users?')) reutrn;
                    url = "TestOne/DeleteAllData";
                    msg = "records deleted.";
                    break;
                default:
                    break;
            }
            $.ajax({
                url: url,
                success: (res) => {
                    reloadTable();
                    if (eventId != "DeleteAllData") makeToast(res.count + " " + msg);
                    else makeToast("All employees deleted successfully.");
                }
            });
            
        })
    })
})