$("#openTransactionModalBtn").on("click", function () {
    console.log('t modal');
    $("#addTransactionModal").modal("show");
});

$("#openCategoryModalBtn").on("click", function () {
    $("#addCategoryModal").modal("show");
});

$(".updateTransaction").on("click", function () {
    var id = $(this).closest('tr').find('td:first').html();
    var date = $(this).closest('tr').find('td:eq(1) .transaction-date').html();
    var amount = $(this).closest('tr').find('.transaction-amount div').html();
    var name = $(this).closest('tr').find('.transaction-name').html();
    var categoryId = $(this).closest('tr').find('td:eq(4)').html();
    var transactionType = $(this).closest('tr').find('#transaction-type').html();

    $('#insert-transaction-form #InsertTransaction_Id').val($.trim(id));
    $('#insert-transaction-form #InsertTransaction_Date').val($.trim(date));
    $('#insert-transaction-form #InsertTransaction_Amount').val($.trim(amount).slice(1, -1));
    $('#insert-transaction-form #InsertTransaction_Name').val($.trim(name));
    $(`#insert-transaction-form #InsertTransaction_CategoryId option[value=${categoryId}]`).attr('selected', 'selected');
    $('#insert-transaction-form #InsertTransaction_TransactionType').find(`option:contains('${$.trim(transactionType)}')`).attr('selected', 'selected');
    $("#addTransactionModal").modal("show");
});

$(".openUpdateCategoryModalBtn").on("click", function () {
    var id = $(this).closest('tr').find('td:first').html();
    var name = $(this).closest('tr').find('td:eq(1)').html();

    $('#insert-category-form #InsertCategory_Id').val($.trim(id));
    $('#insert-category-form #InsertCategory_Name').val($.trim(name));

    $("#addCategoryModal").modal("show");
});

$(".openDeleteCategoryModalBtn").on("click", function () {
    var id = $(this).closest('tr').find('td:first').html();
    $('#deleteCategoryForm').append(`<input type="hidden" name="id" value="${id}">`);
    $("#deleteCategoryModal").modal("show");
});

$(".deleteTransaction").on("click", function(){
    var id = $(this).closest('tr').find('td:first').html();

    $('#deleteTransactionForm').append(`<input type="hidden" name="id" value="${id}">`);
    $("#deleteTransactionModal").modal("show");
});
$("#manageCategories").on("click", function () {
    $("#categories").removeClass("d-none");
    $("#backToTransactions").removeClass("d-none");
    $("#records").addClass("d-none");
    $("#openTransactionModalBtn").addClass("d-none");
    $("#openCategoryModalBtn").removeClass("d-none");
    $("#manageCategories").addClass("d-none");
});

$("#backToTransactions").on("click", function () {
    $("#categories").addClass("d-none");
    $("#backToTransactions").addClass("d-none");
    $("#records").removeClass("d-none");
    $("#openTransactionModalBtn").removeClass("d-none");
    $("#openCategoryModalBtn").addClass("d-none");
    $("#manageCategories").removeClass("d-none");
});