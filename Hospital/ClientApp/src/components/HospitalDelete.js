import React, { Component } from 'react';
import { matchPath } from 'react-router';

export class HospitalDelete extends Component {
    static displayName = HospitalDelete.name;

    constructor(props) {
        super(props);
        this.state = {
            hospital: {},
            loading: true
        };

        var idMatch = matchPath(props.location.pathname, { path: "/hospital-delete/:id", exact: false, strict: false })
        if (idMatch && idMatch.params.id)
            this.state.hospital.id = idMatch.params.id;
        else {
            /* error condition not implemented */
        }

        this.submitForm = this.submitForm.bind(this);
    }

    /*
     * framework pipeline.
     * initiates load of entity
     */
    componentDidMount() {
        this.getHospital(this.state.hospital.id);
    }

    /*
     * return to index
     */
    cancel() {
        window.location.href = "../hospital-data/";
    }

    submitForm() {
        this.deleteHospital(this.state.hospital);
    };

    /*
     * pipeline
     */
    render() {
        /*
         * if the load has not completed, show loading
         */
        if (this.state.loading)
            return <p><em>Loading...</em></p>;

        return (
            <div>
                <h1>{this.state.hospital.title}</h1>
                <p></p>
                <h2 className='emphasis'>will be irreversibly deleted.</h2>
                <h4>Select "Delete" to continue deleting or "Cancel" to go back.</h4>
                <p></p>
                <button className="btn-primary with-margin" onClick={this.submitForm}>Delete</button>
                <button className="btn-primary with-margin" onClick={this.cancel}>Cancel</button>
            </div>
        );
    };

    /*
     * perform the delete operation API call
     */
    async deleteHospital(hospital) {
        const response = await
            fetch('hospital/delete/',
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
        this.cancel();
    };

    async getHospital(hospitalId) {
        const response = await fetch('hospital/Read?hospitalId=' + hospitalId);
        const data = await response.json();
        /* check error condition. not implemented */
        this.setState({ hospital: data, loading: false });
    };
}
