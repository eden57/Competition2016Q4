<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core"%>
<%@ taglib prefix="fmt" uri="http://java.sun.com/jsp/jstl/fmt"%>
<%
	String path = request.getContextPath();
	String basePath = request.getScheme()+"://"+request.getServerName()+":"+request.getServerPort()+path+"/";
%>
<!DOCTYPE html>
<html lang="en">
	<head>
	<base href="<%=basePath%>">
	<!-- jsp文件头和头部 -->
	<%@ include file="../admin/top.jsp"%>   
	</head>
<body>
		
		
<div class="container-fluid" id="main-container">


<div id="page-content" class="clearfix">
						
  <div class="row-fluid">

	<div class="row-fluid">

		
			<table id="table_report" class="table table-striped table-bordered table-hover">
				
				<thead>
					<tr>
						<th class="center">
						<label><input type="checkbox" id="zcheckbox" /><span class="lbl"></span></label>
						</th>
						<th>UID</th>
						<th>PLATE_NO</th>
						<th>PASS_TM</th>
						<th>COLOR</th>
						<th>CREATE_TM</th>
						<th>CREATE_BY</th>
						<th>UPDATE_TM</th>
						<th>UPDATE_BY</th>
						<th>VERSION</th>
						
					</tr>
				</thead>
										
				<tbody>
					
				<!-- 开始循环 -->	
			
					<c:when test="${not empty h2TestList}">
						
						<c:forEach items="${h2TestList}" var="h2" varStatus="vs">
							<tr>
								<td>${user.UID }</td>
								<td>${user.PLATE_NO }</td>
								<td>${user.PASS_TM }</td>
								<td>${user.COLOR }</td>
								<td>${user.CREATE_TM }</td>
								<td>${user.CREATE_BY}</td>
								<td>${user.UPDATE_TM}</td>
								<td>${user.UPDATE_BY }</td>
								<td>${user.VERSION}</td>
							</tr>
						
						</c:forEach>

				</tbody>
			</table>
			
		<div class="page-header position-relative">
		
		</div>
		</form>
	</div>
 
 
 
 
	<!-- PAGE CONTENT ENDS HERE -->
  </div><!--/row-->
	
</div><!--/#page-content-->

		
	</body>
</html>

