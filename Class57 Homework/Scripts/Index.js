$(() => {
    const addPersonToTable = person => {
        $("#people-table").append(`<tr>
                                        <td>
                                            ${person.FirstName}
                                        </td>
                                        <td>
                                            ${person.LastName}
                                        </td>
                                        <td>
                                            ${person.Age}
                                        </td>
                                        <td>
                                            <button style="margin-left: 10px;"
                                                    data-id="${person.Id}"
                                                    data-firstname="${person.FirstName}"
                                                    data-lastname="${person.LastName}"
                                                    data-age="${person.Age}"
                                                    class="btn btn-info show-edit-modal">
                                                Edit
                                            </button>
                                            <button style="margin-left: 10px;" data-id="${person.Id}" class="btn btn-danger delete-person">Delete</button>
                                        </td>
                                    </tr>`);
    }

    $("#add-person").on('click', function () {
        $("#people-table").find("tr:gt(0)").remove();
        const firstName = $("#first-name").val();
        const lastName = $("#last-name").val();
        const age = $("#age").val();
        $.post('/home/AddPerson', { FirstName: firstName, LastName: lastName, Age: age }, function (people) {
            people.forEach(addPersonToTable);
        });
    });

    $("#people-table").on('click', '.show-edit-modal', function () {
        const id = $(this).data('id');
        const firstName = $(this).data('firstname');
        const lastName = $(this).data('lastname');
        const age = $(this).data('age');

        $("#edit-firstname").val(firstName);
        $("#edit-lastname").val(lastName);
        $("#edit-age").val(age);
        $(".update-person").data('id', id);
        $("#edit-person-modal").modal();
    });

    $(".update-person").on('click', function () {
        $("#edit-person-modal").modal('hide');
        $("#people-table").find("tr:gt(0)").remove();
        const firstName = $("#edit-firstname").val();
        const lastName = $("#edit-lastname").val();
        const age = $("#edit-age").val();
        const id = $(".update-person").data('id');
        $.post('/home/UpdatePerson', { FirstName: firstName, LastName: lastName, Age: age, Id: id }, function (people) {
            people.forEach(addPersonToTable);
        });
    });

    $("#people-table").on('click', '.delete-person', function () {
        $("#people-table").find("tr:gt(0)").remove();
        const id = $(this).data('id');
        $.post('/home/DeletePerson', { id: id }, function (people) {
            people.forEach(addPersonToTable);
        });
    });
});