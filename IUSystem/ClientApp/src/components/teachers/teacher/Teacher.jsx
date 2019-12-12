import React from 'react';
import {
  Card, CardImg, CardText, CardBody,
  CardTitle, CardSubtitle, Button
} from 'reactstrap';
import avatar from './assets/avatar.svg'
import { Link } from 'react-router-dom';

const Teacher = (props) => {
  return (
      <Card style={{height: "22em", width: "19em", alignItems: "center", marginTop: "1em", marginBottom: "1em", borderRadius: "1em"}}>
        <CardImg src={avatar} alt="Card image cap" style={{width: "fit-content", height: "10em"}}/>
        <CardBody>
          <CardTitle>{props.name}</CardTitle>
          <CardText>Subjects: {props.lectures.join(', ')}</CardText>
          <Button tag={Link}  to={`/teacher/${props.id}`}>View Lectures</Button>
        </CardBody>
      </Card>   
  );
};

export default Teacher;