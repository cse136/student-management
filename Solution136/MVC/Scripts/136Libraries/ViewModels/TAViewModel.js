function TAViewModel() {

    var TAModelObj = new TAModel();
    var self = this;
    var initialBind = true;
    var taListViewModel = ko.observableArray();

    this.Initialize = function () {

        var viewModel = {
            id: ko.observable(""),
            type: ko.observable(""),
            first: ko.observable(""),
            last: ko.observable(""),
            add: function (data) {
                self.CreateTA(data);
            }
        };

        ko.applyBindings(viewModel, document.getElementById("TAs"));
    };

    this.CreateTA = function (data) {
        var model = {
            id: data.ta_id(),
            type: data.ta_type_id(),
            first: data.first(),
            last: data.last()
        }

        TAModelObj.Create(model, function (result) {
            if (result == "ok") {
                alert("Create ta successful");
            } else {
                alert("Error occurred");
            }
        });
    };

    this.GetAll = function () {
        TAModelObj.GetAll(function (taList) {
            taListViewModel.removeAll();

            for (var i = 0; i < taList.length; i++) {
                taListViewModel.push({
                    id: taList[i].ta_id,
                    type: taList[i].type,
                    name: taList[i].first_name + ' ' + taList[i].last_name,
                });
            }

            if (initialBind) {
                ko.applyBindings({ viewModel: taListViewModel }, document.getElementById("TAs"));
                initialBind = false; // this is to prevent binding multiple time because "Delete" functio calls GetAll again
            }
        });
    };

    this.GetDetail = function (id) {

        StudentModelObj.GetDetail(id, function (result) {

            var student = {
                id: result.StudentId,
                first: result.FirstName,
                last: result.LastName,
                email: result.Email,
                shoesize: result.ShoeSize,
                weight: result.Weight,
                ssn: result.SSN
            };

            if (initialBind) {
                ko.applyBindings({ viewModel: student }, document.getElementById("divStudentContent"));
            }
        });
    };

    ko.bindingHandlers.DeleteStudent = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            $(element).click(function () {
                var id = viewModel.id;

                StudentModelObj.Delete(id, function (result) {
                    if (result != "ok") {
                        alert("Error occurred");
                    } else {
                        studentListViewModel.remove(viewModel);
                    }
                });
            });
        }
    };

    ko.bindingHandlers.DeleteStudentAsync = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            $(element).click(function () {
                var id = viewModel.id;

                StudentModelObj.DeleteAsync(id, function (result) {
                    alert(result);
                    //studentListViewModel.remove(viewModel);
                });
            });
        }
    };

}
