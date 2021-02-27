const Monitor = (function () {
    const remove = function (id) {

        axios.delete(`/monitor/delete/${id}`).then(function () {
            Swal.fire({
                title: 'Delete!',
                text: 'Deleted the monitor',
                icon: 'success',
                confirmButtonText: 'OK'
            }).then(function () {
                location.reload();
            })
        })
    }

    return {
        remove
    }
})();