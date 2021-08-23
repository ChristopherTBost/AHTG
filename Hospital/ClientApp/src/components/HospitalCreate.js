import React, { Component } from 'react';

export class HospitalCreate extends Component {
    static displayName = HospitalCreate.name;

    constructor(props) {
        super(props);
        this.state = {
            hospital: { id: 0, title: 'unknown' },
        };

        this.handleChange = this.handleChange.bind(this);
        this.submitForm = this.submitForm.bind(this);
    }

    /*
     * keep model and view in sync
     */
    handleChange(event) {
        var h = this.state.hospital;
        h.title = event.target.value;
        this.setState({ hospital: h });
    };

    /*
    * return to index
    */
    cancel() {
        window.location.href = "../hospital-data/";
    }

    submitForm() {
        this.createHospital(this.state.hospital);
    };

    render() {

        return (
            <div>
                <h1>{this.state.hospital.title}</h1>

                <fieldset>
                    <label htmlFor="title">Title</label>
                    <input id="title" name="title" type="text" onChange={this.handleChange} defaultValue={this.state.hospital.title} />
                    <p></p>
                </fieldset>
                <button className="btn-primary with-margin" onClick={this.submitForm}>Save</button>
                <button className="btn-primary with-margin" onClick={this.cancel}>Cancel</button>
            </div>
        );
    };

    /*
    * perform the create operation API call
    */
    async createHospital(hospital) {
        const response = await
            fetch('hospital/create/',
                {
                    method: "post",
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(hospital)
                });
        const data = await response.json();
        /* check error condition. not implemented */
        this.setState({ hospital: data });
        this.cancel();
    };
}
