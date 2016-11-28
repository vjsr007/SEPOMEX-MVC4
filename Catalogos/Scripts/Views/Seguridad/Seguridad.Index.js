"use strict";
App.Angular.controller(
    "SeguridadController",
    ['$scope', '$filter',
    function ($scope, $filter) {

        $scope.filtro = { Nombre: "", Activo: true }

        $scope.init = function () {
            $scope.buscarUsuarios();

            $scope.buscarRolFunciones();
        }

        $scope.buscarUsuarios = function () {
            Utils.ejecutarAjax($scope.filtro, webroot + 'Seguridad/ObtenerUsuarioFunciones', function (data) {
                $scope.$apply(function () {

                    if (data.length > 0) {

                        $scope.usersFunctions = data
                        $scope.users = _.unique(data, "UsuarioID");

                        $scope.user = $scope.users[0];
                        $scope.user.funciones = _.unique(_.where(data, { UsuarioID: $scope.user.UsuarioID }), "FuncionID");
                    }

                });
            });
        }

        $scope.buscarRolFunciones = function (data) {
            Utils.ejecutarAjax(data, webroot + 'Seguridad/ObtenerRolFunciones', function (data) {
                $scope.$apply(function () {
                    $scope.rolFunciones = data;
                    $scope.roles = _.unique(data, "RolID");
                    $scope.funciones = _.unique(data, "FuncionID");
                });
            });
        }

        $scope.bindingUI = function () {
            $(".selectable").selectable({
                stop: function (event, ui) {
                    $(event.target).children('.ui-selected').not(':first').removeClass('ui-selected');
                    var userSelected = $(event.target).children('.ui-selected:first').attr("data-user-selected");

                    var found = $filter('getById')($scope.users, userSelected, 'UsuarioID');

                    $scope.$apply(function () {
                        $scope.user = found;
                        $scope.user.funciones = _.unique(_.where($scope.usersFunctions, { UsuarioID: $scope.user.UsuarioID }), "FuncionID");
                    });

                }
            });
        }

        $scope.changeRol = function () {
            var found = $scope.user.RolID;

            $scope.user.Rol = _.where($scope.roles, { RolID: found })[0].Rol;
            $scope.user.funciones = _.unique(_.where($scope.rolFunciones, { RolID: found }), "FuncionID");
        }

        $scope.guardarUsuario = function () {
            Utils.ejecutarAjax($scope.user, webroot + 'Seguridad/GuardarUsuarioFunciones', function (data) {
                $scope.$apply(function () {

                });
            });
        }

        $scope.init();
    }]
);