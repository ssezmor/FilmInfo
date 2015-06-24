$("[data-bootpag]").each(function () {
    var $this = $(this);
    var options = $this.data("bootpag");
    $this.bootpag(options);
})