
$(document).ready(function () {
    if ("-ms-user-select" in document.documentElement.style && navigator.userAgent.match(/IEMobile\/10\.0/)) {
        var msViewportStyle = document.createElement("style");
        msViewportStyle.appendChild(
            document.createTextNode("@-ms-viewport{width:auto!important}")
        );
        document.getElementsByTagName("head")[0].appendChild(msViewportStyle);
    }
    $("#Roles_1__Selected").click(function () {
        if ($("#Roles_1__Selected").prop('checked') && $("#Roles_2__Selected").prop('checked'))
            $("#Roles_2__Selected").click();
    });
    $("#Roles_2__Selected").click(function () {
        if ($("#Roles_1__Selected").prop('checked') && $("#Roles_2__Selected").prop('checked'))
            $("#Roles_1__Selected").click();
    });
    $(".grid-filter-btn").on("click", function () {
        setTimeout(function () {
            $(".grid-filter-type option[value='1']").remove();
        }, 100);
    });

    $("input[name='attachedFile']").on("change", function () {
        if ($(this).val() != '' && $(this).val() != null) {
            $(this).next("input[name='attachedFile']").show();
            $("#clear-attachments").show();
        }
    });
    $("#clear-attachments").hide();
    $("#clear-attachments").click(function () {
        $(this).hide();
        $("input[name='attachedFile']").val('');
        $("input[name='attachedFile']").hide();
        $("input[name='attachedFile']").each(function () {
            $(this).show();
            return false;
        });
    });
    $("form").submit(function () {
        if ($(this).valid())
            $("input:submit").attr("disabled", true);

    })
    $("#myModal .grid-row").on("click", function () {
        var id = $(this).find('td').eq(1).find('input').val();
        $("#RecepientId").val(id);
        $("#myModal .btn").click();
    });
    if ($("#hiddenSearchField").val() != '') $("#myModalBtn").click();

});