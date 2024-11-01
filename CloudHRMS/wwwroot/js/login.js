$("#logout").on('click', function (e) {
    e.preventDefault(); // Prevent form submission

    Swal.fire({
        title: 'Are you sure?',
        text: "You will be logged out!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, logout!'
    }).then((result) => {
        if (result.isConfirmed) {
            document.getElementById('logoutForm').submit(); // Submit the form
        }
    });
});
