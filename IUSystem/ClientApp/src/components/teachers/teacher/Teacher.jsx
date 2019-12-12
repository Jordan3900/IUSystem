import React from 'react';
import {
  Card, CardImg, CardText, CardBody,
  CardTitle, CardSubtitle, Button
} from 'reactstrap';
import avatar from './assets/avatar.svg'

const Teacher = (props) => {
  return (
      <Card style={{height: "22em", width: "19em", alignItems: "center", marginTop: "1em", marginBottom: "1em", borderRadius: "1em"}}>
        <CardImg src={avatar} alt="Card image cap" style={{width: "fit-content", height: "10em"}}/>
        <CardBody>
          <CardTitle>{props.name}</CardTitle>
          <CardText>Some quick example text to build on the card title and make up the bulk of the card's content.</CardText>
          <Button>View Profile</Button>
        </CardBody>
      </Card>   
  );
};

export default Teacher;