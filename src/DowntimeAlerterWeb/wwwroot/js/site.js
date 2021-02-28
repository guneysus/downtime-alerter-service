const Monitor = (function () {
    const remove = function (id) {

        Swal.fire({
            title: 'Are you sure?',
            text: 'Do you really want to delete the monitor?',
            showConfirmButton: false,
            showDenyButton: true,
            showCancelButton: true,
            icon: 'question',
            denyButtonText: 'Delete',
        }).then(function (result) {
            if (result.isDenied) {
                axios
                    .delete(`/monitor/delete/${id}`)
                    .then(function () {
                        Swal.fire({
                            title: 'Delete!',
                            text: 'Deleted the monitor',
                            icon: 'success',
                            confirmButtonText: 'OK'
                        }).then(function () {
                            location.reload();
                        })
                        
                    });
            }
        });


    }

    return {
        remove
    }
})();