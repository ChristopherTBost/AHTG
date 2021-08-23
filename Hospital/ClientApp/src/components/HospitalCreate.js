import React, { Component } from 'react';

export class HospitalCreate extends Component {
    static displayName = HospitalCreate.name;

  constructor(props) {
    super(props);
      this.state = {
          hospital: { id : 0, title : 'unknown' },
          loading : false
      };

      this.handleChange = this.handleChange.bind(this);
      this.submitForm = this.submitForm.bind(this);
      //this.handleSubmit = this.handleSubmit.bind(this);
  }

    handleChange(event) {
        var h = this.state.hospital;
        h.title = event.target.value;
        this.setState({ hospital: h });
    };

    handleSubmit(event)
    {
        event.preventDefault();
        window.location.href = "/hospital-data";
    };

    cancel() {
        window.location.href = "../hospital-data/";
    }

    submitForm() {
        this.createHospital(this.state.hospital);
    };

    render()
    {
        if (this.state.loading)
            return <p><em>Loading...</em></p>;
    return (
      <div>
            <h1>{this.state.hospital.title}</h1>

            <form ref={ref => this.formRef = ref} method="post" action='../hospital/create/' onSubmit={this.handleSubmit}>
                <label htmlFor="title">Title</label>
                <input id="title" name="title" type="text" onChange={this.handleChange} defaultValue={this.state.hospital.title} />
                <p></p>
            </form>
            <button onClick={this.submitForm}>Save</button>
            <button onClick={this.cancel}>Cancel</button>
      </div>
    );
    };

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
        this.setState({ hospital: data, loading: false });
        this.cancel();
    };
}
