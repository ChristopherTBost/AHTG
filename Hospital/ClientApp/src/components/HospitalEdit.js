import React, { Component } from 'react';
import { matchPath } from 'react-router';

export class HospitalEdit extends Component {
    static displayName = HospitalEdit.name;

    constructor(props) {
        super(props);
        this.state = {
            /* the hospital entity */
            hospital: { id: 0, title: "error" },
            /* view state used during entity get */
            loading: true
        };

        var idMatch = matchPath(props.location.pathname, { path: "/hospital-edit/:id", exact: false, strict: false })
        if (idMatch && idMatch.params.id)
            this.state.hospital.id = idMatch.params.id;
        else {
            /* error condition not implemented */
        }

        this.handleChange = this.handleChange.bind(this);
        this.submitForm = this.submitForm.bind(this);
    }

    /* pipeline point, start loading the entity */
    componentDidMount() {
        this.getHospital(this.state.hospital.id);
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
        this.updateHospital(this.state.hospital);
    };

    /*
     * pipeline
     */
    render() {
        if (this.state.loading)
            return <p><em>Loading...</em></p>;

        return (
            <div>
                <h1>{this.state.hospital.title}</h1>

                <fieldset>
                    <label htmlFor="title">Title</label>
                    <input id="title" name="title" type="text" onChange={this.handleChange} defaultValue={this.state.hospital.title} />
                    <input id="id" name="id" type="hidden" defaultValue={this.state.hospital.id} />
                    <p></p>
                </fieldset>
                <button className="btn-primary with-margin" onClick={this.submitForm}>Save</button>
                <button className="btn-primary with-margin" onClick={this.cancel}>Cancel</button>
            </div>
        );
    };
    /*
    * perform the update operation API call
    */
    async updateHospital(hospital) {
        const response = await
            fetch('hospital/update/',
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
        this.setState({ hospital: data, loading: false });
        this.cancel();
    };

    async getHospital(hospitalId) {
        const response = await fetch('hospital/Read?hospitalId=' + hospitalId);

        const data = await response.json();
        /* check error condition. not implemented */
        this.setState({ hospital: data, loading: false });
    };
}
