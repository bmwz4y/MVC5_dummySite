// moved from inside the Employees/Index.cshtml to here when I saw lesson 87 and lesson 88

$(document).ready(function () {
        $("#deleteAllCheckbox").click(function () {
            $("input[name='idsToDelete']").prop("checked", this.checked);
        });
        $("input[name='idsToDelete']").click(function () {
            debugger; if ($("input[name='idsToDelete']").length === $("input[name='idsToDelete']:checked").length) {
                $("#deleteAllCheckbox").prop("checked", true);
            } else {
                $("#deleteAllCheckbox").prop("checked", false);
            }
        });
        $("#deleteSubmit").click(function () {
            var countDeletions = $("input[name='idsToDelete']:checked").length;
            if (countDeletions === 0) {
                alert("Nothing selected to delete!");
                return false;
            }
            else {
                return confirm(countDeletions + " employees will be deleted!");
            }
        });
});