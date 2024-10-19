function deleteById(id, description) {
    try {
        Swal.fire({
            title: "Are you sure?",
            text: `You want to delete this position ${description}.`,
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Yes, delete it!"
        }).then((result) => {
            if (result.isConfirmed) {
                // Perform the deletion by sending a request to the server
                $.ajax({
                    url: `/position/delete?id=${id}`,
                    type: 'POST',
                    success: function (response) {
                        if (response.success) {
                            Swal.fire({
                                title: "Deleted!",
                                text: "Your file has been deleted.",
                                icon: "success"
                            }).then(() => {
                                // Show toastr message
                                toastr.success('Deleted successfully!');

                                // Wait for a brief moment before reloading the page
                                setTimeout(() => {
                                    location.reload();
                                }, 1500); // 1.5 seconds delay to allow toastr to be seen
                            });
                        } else {
                            Swal.fire({
                                title: "Failed!",
                                text: "Failed to delete the record.",
                                icon: "error"
                            });
                        }
                    },
                    error: function (err) {
                        console.log('Error:', err);
                        toastr.error('An error occurred while deleting the record.');
                    }
                });
            }
        });

    } catch (err) {
        console.log(err);
    }
}

$('#positionTable').on('click', '.btn-danger', function () {
    const id = $(this).data('id');
    const description = $(this).data('description');
    deleteById(id, description);
});
