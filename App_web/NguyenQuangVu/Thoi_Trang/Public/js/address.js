$(document).ready(() => {
    //orderer
    var citis = document.getElementById("city");
    var districts = document.getElementById("district");
    var wards = document.getElementById("ward");
    var cty = document.getElementById("cty");
    var huyen = document.getElementById("quan-huyen");
    var phuong = document.getElementById("xa-phuong"); 
    //var idCity;
    var Parameter = {
        url: "https://raw.githubusercontent.com/kenzouno1/DiaGioiHanhChinhVN/master/data.json",
        method: "GET",
        responseType: "application/json"
    };
    var promise = axios(Parameter);
    promise.then(function (resutl) {
        RenderCity(resutl.data);       
    });

    //orderer
    function RenderCity(data) {
        for (const x of data) {
            citis.options[citis.options.length] = new Option(x.Name, x.Id);           
        };
        citis.onchange = function () {
            districts.length = 1;
            wards.length = 1;
            if (this.value != "") {
                const result = data.filter(n => n.Id === this.value);

                for (const k of result[0].Districts) {
                    districts.options[districts.options.length] = new Option(k.Name, k.Id);                   
                }
                var ctyName = [];
                $.each($(".name-city option:selected"), function () {
                    ctyName.push(this.text);
                });
                cty.value = ctyName;
                $('#cty').html(cty.value);
            }           
        };
        districts.onchange = function () {
            wards.length = 1;
            const dataCity = data.filter((n) => n.Id === citis.value);
            if (this.value != "") {
                const dataWards = dataCity[0].Districts.filter(n => n.Id === this.value)[0].Wards;

                for (const w of dataWards) {
                    wards.options[wards.options.length] = new Option(w.Name, w.Id);
                }
                var dist = [];
                $.each($(".district option:selected"), function () {
                    dist.push(this.text);
                });
                huyen.value = dist;
                $('#quan-huyen').html(huyen.value);
            }
        };
        wards.onchange = function () {
            if (this.value != "") {
                var xa = [];
                $.each($(".ward option:selected"), function () {
                    xa.push(this.text);
                });
                phuong.value = xa;
                $('#xa-phuong').html(phuong.value);
            }
        }
    }
});

